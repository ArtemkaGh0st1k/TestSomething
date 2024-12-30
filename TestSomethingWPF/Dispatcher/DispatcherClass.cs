namespace TestSomethingWPF.Dispatcher
{
    public static class DispatcherClass
    {
        static DispatcherClass()
        {
            Dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
        }

        public static System.Windows.Threading.Dispatcher Dispatcher { get; set; }
    }
}
