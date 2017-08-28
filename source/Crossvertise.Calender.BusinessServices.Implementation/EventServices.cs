using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Crossvertise.Calender.BusinessServices.Core.Exceptions;
using Crossvertise.Calender.DAL.Domain;
using Crossvertise.Calender.DAL.Domain.UnitOfWork;
using Crossvertise.Calender.BusinessServices.Core.Models;
using Crossvertise.Calender.BusinessServices.Core.Services;

namespace Crossvertise.Calender.BusinessServices.Implementation
{
    public class EventServices : IEventServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public EventServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventModel>();
                cfg.CreateMap<EventAttendee, EventAttendeeModel>();
                cfg.CreateMap<Event, EventDetailsModel>().ForMember(a => a.Attendees, b => b.ResolveUsing(c => c.Attendees));
            });
            mapper = config.CreateMapper();
        }

        public IEnumerable<EventModel> GetMonthEvents(int monthId)
        {
            if(monthId < 1 || monthId > 12)
                throw new InvalidMonthException(); 

            var events = _unitOfWork.EventRepository.GetMany(e =>
                e.EventDateTime.Year == DateTime.Now.Year && e.EventDateTime.Month == monthId);

            if (events != null && events.Any())
                return mapper.Map<List<Event>, List<EventModel>>(events.ToList());

            return null;
        }

        public EventDetailsModel GetEventDetails(EventModel eventModel)
        {
            return GetEventDetails(eventModel.Id);
        }

        public EventDetailsModel GetEventDetails(int eventId)
        {
            var eventEntity = _unitOfWork.EventRepository.GetWithInclude(e => e.Id == eventId, "Attendees");
            if (eventEntity != null && eventEntity.Any())
            {
                return mapper.Map<Event, EventDetailsModel>(eventEntity.First());
            }
            return null;
        }

    }
}
