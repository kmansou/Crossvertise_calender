using Microsoft.Practices.Unity;
using System.Web.Http;
using Crossvertise.Calender.BusinessServices.Core.Services;
using Crossvertise.Calender.BusinessServices.Implementation;
using Crossvertise.Calender.DAL.Domain.UnitOfWork;
using Crossvertise.Calender.DAL.EF.UnitOfWork;
using Unity.WebApi;

namespace Crossvertise.Calender.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, EFxCalenderUoW>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventServices, EventServices>(new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}