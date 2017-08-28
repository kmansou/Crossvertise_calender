using System.ComponentModel.Composition;
using Crossvertise.Calender.DAL.Domain.UnitOfWork;
using Crossvertise.Calender.DAL.EF.UnitOfWork;
using Crossvertise.Calender.DependencyResolver;

namespace Crossvertise.Calender.DAL.EF
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterTypeWithControlledLifeTime<IUnitOfWork, EFxCalenderUoW>();

        }
    }
}
