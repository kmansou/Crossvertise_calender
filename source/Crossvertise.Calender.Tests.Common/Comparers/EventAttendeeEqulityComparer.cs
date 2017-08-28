using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossvertise.Calender.DAL.Domain;

namespace Crossvertise.Calender.Tests.Common.Comparers
{
    public class EventAttendeeEqulityComparer : IEqualityComparer<EventAttendee>
    {
        public bool Equals(EventAttendee x, EventAttendee y)
        {
            return x.Id == y.Id &&
                   x.EventId == y.EventId &&
                   x.AttendeeFullName == y.AttendeeFullName &&
                   x.AttendeeOrder == y.AttendeeOrder;
        }

        public int GetHashCode(EventAttendee obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.EventId.GetHashCode() ^
                   obj.AttendeeFullName.GetHashCode() ^
                   obj.AttendeeOrder.GetHashCode();
        }
    }
}
