namespace Crossvertise.Calender.DependencyResolver
{
    public interface IComponent
    {
        /// <summary>
        /// Register underlying types with unity.
        /// </summary>
        void SetUp(IRegisterComponent registerComponent);
    }
}
