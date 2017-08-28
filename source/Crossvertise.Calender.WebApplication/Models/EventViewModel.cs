using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crossvertise.Calender.WebApplication.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime EventDateTime { get; set; }
    }
}