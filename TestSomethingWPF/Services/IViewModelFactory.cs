using System;

namespace TestSomethingWPF.Services
{
    public interface IViewModelFactory
    {
        T Create<T>();
    }

    public class ViewModelFactory : IViewModelFactory
    {
        public T Create<T>()
        {  
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
