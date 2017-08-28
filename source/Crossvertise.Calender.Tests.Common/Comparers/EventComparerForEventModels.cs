using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossvertise.Calender.DAL.Domain;

namespace Crossvertise.Calender.Tests.Common.Comparers
{
    public class EventComparerForEventModels : IComparer, IComparer<Event>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Event;
            var rhs = actual as Event;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Event expected, Event actual)
        {
            int temp = expected.Id.CompareTo(actual.Id);
            if (temp == 0)
            {
                temp = expected.Description.CompareTo(actual.Description);
                if (temp == 0)
                {
                    return expected.EventDateTime.CompareTo(actual.EventDateTime);
                }
            }
            return temp;
        }
    }
}
