using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossvertise.Calender.BusinessServices.Core.Exceptions;
using Crossvertise.Calender.BusinessServices.Core.Models;
using Crossvertise.Calender.BusinessServices.Core.Services;
using Crossvertise.Calender.DAL.Domain;
using Crossvertise.Calender.DAL.Domain.UnitOfWork;
using Crossvertise.Calender.DAL.EF.Context;
using Crossvertise.Calender.DAL.EF.Repository;
using NUnit.Framework;
using Crossvertise.Calender.Tests.Common;
using Crossvertise.Calender.BusinessServices.Implementation;
using Crossvertise.Calender.Tests.Common.Comparers;

namespace Crossvertise.Calender.BusinessServices.Tests
{
    public class EventServicesTests
    {
        #region Variables
        private IEventServices _eventServices;
        private IUnitOfWork _unitOfWork;
        private List<Event> _events;
        private EventRepository _eventRepository;
        private CalenderDbContext _dbContext;
        private readonly IMapper mapper;
        #endregion

        public EventServicesTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EventModel, Event>();
                cfg.CreateMap<EventAttendeeModel, EventAttendee>();
                cfg.CreateMap<EventDetailsModel, Event>()
                    .ForMember(a => a.Attendees, b => b.ResolveUsing(c => c.Attendees));
            });
            mapper = config.CreateMapper();
        }

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            _events = DataInitializers.SetUpEvents();
        }


        /// <summary>
        /// TestFixture teardown
        /// </summary>
        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            _events = null;
        }

        /// <summary>
        /// Re-initializes test.
        /// </summary>
        [SetUp]
        public void ReInitializeTest()
        {
            _dbContext = DataInitializers.SetCalenderDbContext();
            _eventRepository = DataInitializers.SetUpEventRepository(_dbContext, _events);
            _unitOfWork = DataInitializers.SetUnitOfWork(_eventRepository);
            _eventServices = new EventServices(_unitOfWork);
        }

        /// <summary>
        /// Tears down each test data
        /// </summary>
        [TearDown]
        public void DisposeTest()
        {
            _eventServices = null;
            _unitOfWork = null;
            _eventRepository = null;
            if (_dbContext != null)
                _dbContext.Dispose();
        }


        [Test]
        public void GetMonthEventsTest()
        {
            // given
            int monthId = 2;
            var eventComparer = new EventComparerForEventModels();
            var expectedResult = _events
                .FindAll(e => e.EventDateTime.Year == DateTime.Now.Year && e.EventDateTime.Month == monthId)
                .OrderBy(e => e, eventComparer);

            // when
            var events = _eventServices.GetMonthEvents(monthId);

            // then
            Assert.IsNotNull(events);
            CollectionAssert.IsNotEmpty(events);

            var eventModels = mapper.Map<List<EventModel>, List<Event>>(events.ToList());
            var actualResult = eventModels
                .OrderBy(e => e, eventComparer);

            CollectionAssert.AreEqual(actualResult, expectedResult, eventComparer);
        }

        [Test]
        public void GetMonthEventsTestForNull()
        {
            // given
            int monthId = 4;

            // when
            var events = _eventServices.GetMonthEvents(monthId);

            // then
            Assert.IsNull(events);
        }

        [Test]
        public void GetMonthEvents_InvalidMonthId_ThrowsException()
        {
            // given there's no month with Id = 13 , i.e maxId = 12
            int monthId = 13;

            // expect
            Assert.That(() => _eventServices.GetMonthEvents(monthId),
                Throws.TypeOf<InvalidMonthException>());
        }

        [Test]
        public void GetEventDetailsByRightId()
        {
            // given
            int eventId = 1;
            var eventComparer = new EventEqualityComparerForEventDetails();
            var expectedResult = _events.Find(e => e.Id == eventId);

            // when
            var eventDetails = _eventServices.GetEventDetails(eventId);

            // then
            Assert.IsNotNull(eventDetails);
            var actualResult = mapper.Map<EventDetailsModel, Event>(eventDetails);
            Assert.IsTrue(eventComparer.Equals(actualResult, expectedResult));
        }

        [Test]
        public void GetEventDetailsByWrongId()
        {
            // given, no eventId in mock data = 10
            int eventId = 10;

            // when
            var eventDetails = _eventServices.GetEventDetails(eventId);

            // then
            Assert.Null(eventDetails);
        }
    }
}
