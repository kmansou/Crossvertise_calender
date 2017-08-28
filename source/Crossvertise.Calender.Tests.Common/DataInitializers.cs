using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Crossvertise.Calender.DAL.Domain;
using Crossvertise.Calender.DAL.Domain.UnitOfWork;
using Crossvertise.Calender.DAL.EF.Context;
using Crossvertise.Calender.DAL.EF.Repository;
using Moq;

namespace Crossvertise.Calender.Tests.Common
{
    public class DataInitializers
    {
        private static List<Event> getMockEvents()
        {
            var events = new List<Event>()
            {
                new Event()
                { 
                    Description= "Release Planning Meeting",  
                    EventDateTime = DateTime.ParseExact("2017-01-01 13:00:00", "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None),
                    OrganizerFullName = "Ewald",
                    Attendees = new List<EventAttendee>()
                    {
                        new EventAttendee()
                        {
                            AttendeeFullName = "Anwar",
                            AttendeeOrder = 1
                        },
                        new EventAttendee()
                        {
                            AttendeeFullName = "Owis",
                            AttendeeOrder = 2
                        },
                        new EventAttendee()
                        {
                            AttendeeFullName = "Saad",
                            AttendeeOrder = 3
                        }
                    }
                },
                new Event()
                { 
                    Description= "Spring Planning Meeting",  
                    EventDateTime = DateTime.ParseExact("2017-02-08 13:00:00", "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None),
                    OrganizerFullName = "Anwar",
                    Attendees = new List<EventAttendee>()
                    {
                        new EventAttendee()
                        {
                            AttendeeFullName = "Karim",
                            AttendeeOrder = 1
                        },
                        new EventAttendee()
                        {
                            AttendeeFullName = "Emad",
                            AttendeeOrder = 2
                        },
                        new EventAttendee()
                        {
                            AttendeeFullName = "Moharam",
                            AttendeeOrder = 3
                        },
                        new EventAttendee()
                        {
                            AttendeeFullName = "Nabil",
                            AttendeeOrder = 4
                        }
                    }
                },
                new Event()
                { 
                    Description= "Company establishment Anniversary",  
                    EventDateTime = DateTime.ParseExact("2017-03-28 17:00:00", "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None),
                    OrganizerFullName = "Wael",
                    Attendees = new List<EventAttendee>()
                    {
                        new EventAttendee()
                        {
                            AttendeeFullName = "All Company",
                            AttendeeOrder = 1
                        }
                    }
                }
            };
            return events;
        }

        public static CalenderDbContext SetCalenderDbContext()
        {
            return new Mock<CalenderDbContext>().Object;
        }
        public static List<Event> SetUpEvents()
        {
            var eventId = new int();
            var eventAttendeeId = new int();
            var events = getMockEvents();
            foreach (Event eventEntity in events)
            {
                eventEntity.Id = ++eventId;
                foreach (var eventAttendee in eventEntity.Attendees)
                {
                    eventAttendee.Id = ++eventAttendeeId;
                    eventAttendee.EventId = eventId;
                }
            }

            return events;
        }

        public static EventRepository SetUpEventRepository(CalenderDbContext dbContext, List<Event> events)
        {
            // Initialise repository
            var mockRepo = new Mock<EventRepository>(MockBehavior.Default, dbContext);

            // Setup mocking behavior
            mockRepo.Setup(e => e.GetAll()).Returns(events);

            mockRepo.Setup(e => e.Get(It.IsAny<int>()))
                .Returns(new Func<int, Event>(
                    id => events.Find(e => e.Id.Equals(id))));

            mockRepo.Setup(e => e.GetMany(It.IsAny<Expression<Func<Event, bool>>>()))
                .Returns(new Func<Expression<Func<Event, bool>>, IEnumerable<Event>>(
                    expression => events.Where(expression.Compile()).ToList()));

            mockRepo.Setup(e => e.GetWithInclude(It.IsAny<Expression<Func<Event, bool>>>(), It.IsAny<string[]>()))
                .Returns((Expression<Func<Event, bool>> predicate, string[] include) => events.Where(predicate.Compile()).ToList());


            // Return mock implementation object
            return mockRepo.Object;
        }

        public static IUnitOfWork SetUnitOfWork(EventRepository eventRepository)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.EventRepository).Returns(eventRepository);
            return unitOfWork.Object;
        }
    }
}
