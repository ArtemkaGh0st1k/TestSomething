using System;

namespace Event.Observer.TemperatureExample
{
    public class TemperatureReporter : IObserver<Temperature>
    {
        private IDisposable _unsubscriber;
        private bool _first = true;
        private Temperature _last;

        public void OnCompleted()
        {
            Console.WriteLine("Выполнился метод OnCompleted!");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Выполнился метод OnError!");
        }

        public void OnNext(Temperature value)
        {
            Console.WriteLine("The temperature is {0}C at {1:g}", value.Degress, value.Date);
            if (_first)
            {
                _last = value;
                _first = false;
            }
            else
                Console.WriteLine("     Change: {0} in {1:g}", value.Degress - _last.Degress,
                    value.Date.ToUniversalTime() - _last.Date.ToUniversalTime());
        }

        public virtual void Subscribe(IObservable<Temperature> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
    }
}
