using Crossvertise.Calender.BusinessServices.Core.Services;
using Crossvertise.Calender.BusinessServices.Implementation;
using Crossvertise.Calender.DAL.EF.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crossvertise.Calender.BusinessServices.Core.Models;

namespace Crossvertise.Calender.WebApi.Controllers
{
    public class EventsController : ApiController
    {
        private readonly IEventServices _eventServices;

        public EventsController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        // GET api/Events/GetMonthEvents?monthId={monthId}
        public HttpResponseMessage GetMonthEvents(int monthId)
        {
            var events = _eventServices.GetMonthEvents(monthId);
            if (events != null)
            {
                var eventsList = events as List<EventModel> ?? events.ToList();
                if (eventsList.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, eventsList);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Events in this month");
        }

        // GET api/Events/GetEventDetails?eventId={eventId}
        public HttpResponseMessage GetEventDetails(int eventId)
        {
            var eventDetails = _eventServices.GetEventDetails(eventId);
            if (eventDetails != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, eventDetails);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Couldn't find event details");
        }
    }
}
