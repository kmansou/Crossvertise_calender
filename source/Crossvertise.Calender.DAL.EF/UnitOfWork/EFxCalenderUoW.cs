using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using Crossvertise.Calender.DAL.Domain.Repository;
using Crossvertise.Calender.DAL.Domain.UnitOfWork;
using Crossvertise.Calender.DAL.EF.Context;
using Crossvertise.Calender.DAL.EF.Repository;

namespace Crossvertise.Calender.DAL.EF.UnitOfWork
{
    public class EFxCalenderUoW : IUnitOfWork
    {
        private readonly CalenderDbContext calenderDbContext;
        private IEventRepository eventRepository;

        public IEventRepository EventRepository
        {
            get { return eventRepository ?? (eventRepository = new EventRepository(calenderDbContext)); }
        }

        public EFxCalenderUoW()
        {
            calenderDbContext = new CalenderDbContext();
        }

        public void Save()
        {
            try
            {
                calenderDbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName,
                            ve.ErrorMessage));
                    }
                }
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }
    }
}
