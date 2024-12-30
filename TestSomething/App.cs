using System.Reflection;
using System.Windows;
using TestSomething.Reflcecton;

namespace TestSomething
{
    public class App
    {
        public static void Main()
        {
            MessageBox.Show(string.Format("Текущая запускаемая сборка: {0}", Assembly.GetExecutingAssembly().GetName().Name),
                            "Уведомление",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            var reflcetionTest = new ReflectionTest();
            reflcetionTest.Test();
        }
    }
}
