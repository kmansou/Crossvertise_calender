using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossvertise.Calender.BusinessServices.Core.Exceptions
{
    public class InvalidMonthException : Exception
    {
        public InvalidMonthException()
            : base("Invalid month Id, month should be greater than 1 and less than 12")
        {
            
        }
    }
}
