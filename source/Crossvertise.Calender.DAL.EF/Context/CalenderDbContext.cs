using Crossvertise.Calender.DAL.Domain;
using System.Data.Entity;

namespace Crossvertise.Calender.DAL.EF.Context
{
    public class CalenderDbContext : DbContext
    {
        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventAttendee> Attendees { get; set; }

        public CalenderDbContext()
            : base("name=CalenderDbEntities")
        {

        }
    }
}
