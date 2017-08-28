using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crossvertise.Calender.WebApplication.Models
{
    public class MonthEventListViewModel
    {
        public int MonthId { get; set; }

        public EventViewModel[] Events { get; set; }
    }
}