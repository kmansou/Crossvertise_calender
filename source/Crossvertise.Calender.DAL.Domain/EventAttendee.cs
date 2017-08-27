using System;

namespace Crossvertise.Calender.DAL.Domain
{
    public class EventAttendee
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string AttendeeFullName { get; set; }

        public Nullable<int> AttendeeOrder { get; set; }
    }
}
