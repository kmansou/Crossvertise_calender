using Microsoft.Practices.Unity;
using System.Web.Http;
using Crossvertise.Calender.BusinessServices.Core.Services;
using Crossvertise.Calender.DAL.Domain.UnitOfWork;
using Unity.WebApi;
using Crossvertise.Calender.DependencyResolver;
using System.Configuration;

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
            regeisterTypes(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void regeisterTypes(IUnityContainer container)
        {
            var appSettings = ConfigurationManager.AppSettings;
            ComponentLoader.LoadContainer(container, ".\\bin", appSettings["DataAccessLayerImplementation"]);
            ComponentLoader.LoadContainer(container, ".\\bin", appSettings["BusinessLayerImpelementation"]);
        }
    }
}