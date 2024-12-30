using System.Windows;
using GalaSoft.MvvmLight.Threading;
using TestSomethingWPF.Services;
using TestSomethingWPF.ViewModels;

namespace TestSomethingWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IViewModelFactory _viewModelFactory { get; private set; }
        public App()
        {
            _viewModelFactory = new ViewModelFactory();
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.Initialize();
            var exViewModel = new ExampleViewModel();
            
        }
    }
}
