using Crossvertise.Calender.DAL.Domain.Repository;

namespace Crossvertise.Calender.DAL.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEventRepository EventRepository { get; }

        void Save();
    }
}
