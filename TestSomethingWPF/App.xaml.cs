using System.Windows;
using GalaSoft.MvvmLight.Threading;
using TestSomething.ToNIPI.Services;
using TestSomethingWPF.ViewModels;
using Unity;

namespace TestSomethingWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UnityContainer Container { get; private set; }

        public App()
        {
            Startup += App_Startup;
            SetupIoC();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.Initialize();
            var exViewModel = new ExampleViewModel();
            
        }

        private static UnityContainer SetupIoC()
        {
            Container = new UnityContainer();

            Container.RegisterType<IViewModelFactory, ViewModelFactory>();
            Container.RegisterType<IViewService, ViewService>();

            return Container;
        }
    }
}
