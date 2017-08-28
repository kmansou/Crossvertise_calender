using System;
using System.Collections.Generic;

namespace Crossvertise.Calender.BusinessServices.Core.Models
{
    public class EventDetailsModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime EventDateTime { get; set; }

        public string OrganizerFullName { get; set; }

        public IEnumerable<EventAttendeeModel> Attendees { get; set; }

        public EventDetailsModel()
        {
            Attendees = new List<EventAttendeeModel>();
        }
    }
}
