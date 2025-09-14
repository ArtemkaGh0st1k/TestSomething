using Unity;

namespace TestSomething.ToNIPI.Services
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly UnityContainer _container;

        public ViewModelFactory(UnityContainer container)
        {
            _container = container;
        }

        public T Creaate<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
