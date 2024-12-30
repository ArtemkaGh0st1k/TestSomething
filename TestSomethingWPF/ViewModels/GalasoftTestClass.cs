using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using Unity;

namespace TestSomethingWPF.ViewModels
{
    internal class TestGalasoftClassViewModel : ViewModelBase
    {
        public TestGalasoftClassViewModel()
        {
          
        }
    }

    internal class TestGalasoftClassObservableObject : ObservableObject
    {
        public TestGalasoftClassObservableObject()
        {
            var container = new UnityContainer();
            
        }
    }
}
