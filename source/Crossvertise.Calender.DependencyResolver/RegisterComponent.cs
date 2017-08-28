using Microsoft.Practices.Unity;

namespace Crossvertise.Calender.DependencyResolver
{
    internal class UnityRegisterComponent : IRegisterComponent
    {
        private readonly IUnityContainer _container;

        public UnityRegisterComponent(IUnityContainer container)
        {
            this._container = container;
            //Register interception behaviour if any
        }

        public void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            if (withInterception)
            {
                //register with interception
                RegisterTypeWithControlledLifeTime<TFrom, TTo>();
            }
            else
            {
                this._container.RegisterType<TFrom, TTo>();
            }
        }

        public void RegisterTypeWithControlledLifeTime<TFrom, TTo>() where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
    }
}
