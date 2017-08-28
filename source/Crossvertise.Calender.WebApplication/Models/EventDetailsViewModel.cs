using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crossvertise.Calender.WebApplication.Models
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }

        public MonthEventListViewModel MonthEventList { get; set; }

        public string Description { get; set; }

        [Display(Name= "Date")]
        public DateTime EventDateTime { get; set; }

        [Display(Name = "Organizer")]
        public string OrganizerFullName { get; set; }

        public IEnumerable<EventAttendeeViewModel> Attendees { get; set; }

        public EventDetailsViewModel()
        {
            Attendees = new List<EventAttendeeViewModel>();
        }
    }
}