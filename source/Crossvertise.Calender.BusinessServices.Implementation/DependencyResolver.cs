using System.ComponentModel.Composition;
using Crossvertise.Calender.BusinessServices.Core.Services;
using Crossvertise.Calender.DependencyResolver;

namespace Crossvertise.Calender.BusinessServices.Implementation
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterTypeWithControlledLifeTime<IEventServices, EventServices>();

        }
    }
}
