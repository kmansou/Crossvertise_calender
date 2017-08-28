using System;

namespace Crossvertise.Calender.BusinessServices.Core.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime EventDateTime { get; set; }
    }
}
