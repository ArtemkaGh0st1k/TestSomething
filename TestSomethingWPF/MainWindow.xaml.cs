using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestSomethingWPF.Dispatcher;

namespace TestSomethingWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show(string.Format("Текущая запускаемая сборка: {0}", Assembly.GetExecutingAssembly().GetName().Name),
                            "Уведомление",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            var disp = DispatcherClass.Dispatcher;

            disp.Invoke(() => Tasks.TaskAsync1);
            disp.InvokeAsync(() => Tasks.TaskAsync1);
            Dispatcher.InvokeAsync(() => Tasks.TaskAsync1);
            Dispatcher.InvokeAsync(() => Tasks.TaskAsync2);
            Dispatcher.InvokeAsync(() => Tasks.TaskAsync3);
        }
    }
}
