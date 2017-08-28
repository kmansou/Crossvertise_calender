using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossvertise.Calender.DAL.Domain;

namespace Crossvertise.Calender.Tests.Common.Comparers
{
    public class EventEqualityComparerForEventDetails : IEqualityComparer<Event>
    {
        public bool Equals(Event x, Event y)
        {
            bool result = x.Id == y.Id &&
                          x.Description == y.Description &&
                          x.EventDateTime == y.EventDateTime &&
                          x.OrganizerFullName == y.OrganizerFullName &&
                          ((x.Attendees != null && y.Attendees != null && x.Attendees.Count == y.Attendees.Count) || (x.Attendees == null && y.Attendees == null));

            if (result && x.Attendees != null)
            {
                var eventAttendeeEqualityComparer = new EventAttendeeEqulityComparer();
                for (int i = 0; i < x.Attendees.Count; i++)
                {
                    result = result && eventAttendeeEqualityComparer.Equals(x.Attendees.ElementAt(i), y.Attendees.ElementAt(i));
                    if (!result)
                        break;
                }
            }

            return result;
        }

        public int GetHashCode(Event obj)
        {
            int result = obj.Id.GetHashCode() ^
                         (obj.Description == null ? 0 : obj.Description.GetHashCode()) ^
                         obj.EventDateTime.GetHashCode() ^
                         (obj.OrganizerFullName == null ? 0 : obj.OrganizerFullName.GetHashCode());

            var eventAttendeeEqualityComparer = new EventAttendeeEqulityComparer();
            if (obj.Attendees != null && obj.Attendees.Any())
            {
                foreach (var attendee in obj.Attendees)
                {
                    result = result ^ eventAttendeeEqualityComparer.GetHashCode(attendee);
                }
            }

            return result;
        }
    }
}
