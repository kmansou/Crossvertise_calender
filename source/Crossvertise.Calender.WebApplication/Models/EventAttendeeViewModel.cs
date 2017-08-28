using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crossvertise.Calender.WebApplication.Models
{
    public class EventAttendeeViewModel
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string AttendeeFullName { get; set; }

        public int? AttendeeOrder { get; set; }
    }
}