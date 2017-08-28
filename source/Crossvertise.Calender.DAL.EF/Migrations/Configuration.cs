using System.Collections.Generic;
using System.Globalization;
using Crossvertise.Calender.DAL.Domain;

namespace Crossvertise.Calender.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Crossvertise.Calender.DAL.EF.Context.CalenderDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Crossvertise.Calender.DAL.EF.Context.CalenderDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Events.AddOrUpdate(e => e.Description,
                new Event()
                {
                    Description = "Interview with Karim",
                    EventDateTime = DateTime.ParseExact("2017-08-23 15:30:00", "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None),
                    OrganizerFullName = "Chresitna Gayer",
                    Attendees = new List<EventAttendee>()
                    {
                        new EventAttendee(){AttendeeFullName = "Pavel Morshenyuk", AttendeeOrder = 1},
                        new EventAttendee(){AttendeeFullName = "Karim Mansour", AttendeeOrder =  2}
                    }
                });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
