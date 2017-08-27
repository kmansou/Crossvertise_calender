using System;
using System.Collections.Generic;

namespace Crossvertise.Calender.DAL.Domain
{
    public class Event
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime EventDateTime { get; set; }

        public string OrganizerFullName { get; set; }

        public ICollection<EventAttendee> Attendees { get; set; }

        public Event()
        {
            Attendees = new List<EventAttendee>();
        }
    }
}
