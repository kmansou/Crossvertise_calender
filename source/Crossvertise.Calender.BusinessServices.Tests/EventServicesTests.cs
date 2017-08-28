using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //if (_dbContext != null)
              //  _dbContext.Dispose();
        }


        [Test]
        public void GetMonthEventsTest()
        {
            var events = _eventServices.GetMonthEvents(2);
            if (events != null)
            {
                var eventModels = mapper.Map<List<EventModel>, List<Event>>(events.ToList());
                var eventComparer = new EventComparerForEventModels();
                var actualResult = eventModels
                    .OrderBy(e => e, eventComparer);
                var expectedResult = _events
                    .FindAll(e => e.EventDateTime.Year == DateTime.Now.Year && e.EventDateTime.Month == 2)
                    .OrderBy(e => e, eventComparer);

                CollectionAssert.AreEqual(actualResult, expectedResult, eventComparer);
            }
        }

        [Test]
        public void GetMonthEventsTestForNull()
        {
            var events = _eventServices.GetMonthEvents(13);
            Assert.Null(events);
        }

        [Test]
        public void GetEventDetailsByRightId()
        {
            var eventDetails = _eventServices.GetEventDetails(1);
            if (eventDetails != null)
            {
                var actualResult = mapper.Map<EventDetailsModel, Event>(eventDetails);
                var eventComparer = new EventEqualityComparerForEventDetails();
                var expectedResult = _events.Find(e => e.Id == 1);

                Assert.IsTrue(eventComparer.Equals(actualResult, expectedResult));
            }
        }

        [Test]
        public void GetEventDetailsByWrongId()
        {
            var eventDetails = _eventServices.GetEventDetails(10);
            Assert.Null(eventDetails);
        }
    }
}
