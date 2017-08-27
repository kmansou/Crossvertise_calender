using Crossvertise.Calender.DAL.Domain;
using Crossvertise.Calender.DAL.Domain.Repository;
using Crossvertise.Calender.DAL.EF.Context;

namespace Crossvertise.Calender.DAL.EF.Repository
{
    public class EventRepository : GenericReadOnlyRepository<Event>, IEventRepository
    {
        public EventRepository(CalenderDbContext context)
            : base(context)
        {

        }
    }
}
