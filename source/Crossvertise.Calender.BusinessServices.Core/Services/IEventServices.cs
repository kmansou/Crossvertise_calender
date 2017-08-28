using System.Collections.Generic;
using Crossvertise.Calender.BusinessServices.Core.Models;

namespace Crossvertise.Calender.BusinessServices.Core.Services
{
    public interface IEventServices
    {
        IEnumerable<EventModel> GetMonthEvents(int month);

        EventDetailsModel GetEventDetails(int eventId);

        EventDetailsModel GetEventDetails(EventModel eventModel);
    }
}
