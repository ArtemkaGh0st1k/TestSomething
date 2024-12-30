using System;
using System.Collections.Generic;
using System.Threading;

namespace Event.Observer.TemperatureExample
{
    public class TemperatureMonitor : IObservable<Temperature>
    {
        public List<IObserver<Temperature>> _observers;

        public TemperatureMonitor()
        {
            _observers = new List<IObserver<Temperature>>();
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Temperature>> _observers;
            private IObserver<Temperature> _observer;

            public Unsubscriber(List<IObserver<Temperature>> observers, IObserver<Temperature> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);    
            }
        }

        public IDisposable Subscribe(IObserver<Temperature> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber(_observers, observer);
        }

        public void GetTemperature()
        {
            var temps = new List<double?>()
            {
                14.6d, 14.65d, 14.7d, 14.9d, 14.9d, 15.2d,
                15.25d, 15.2d, 15.4d, 15.45d, null
            };

            double? previous = null;
            var start = true;

            foreach (var temp in temps)
            {
                Thread.Sleep(2500);
                if (temp.HasValue)
                {
                    if (start || Math.Abs(temp.Value - previous.Value) >= 0.1d)
                    {
                        var tempDate = new Temperature(temp.Value, DateTime.Now);
                        foreach (var observer in _observers)
                        {
                            observer.OnNext(tempDate);
                        }
                        previous = temp;
                        if (start) start = !start;
                    }
                }
                else
                {
                    foreach (var observer in _observers.ToArray())
                    {
                        if (observer != null) observer.OnCompleted();
                    }
                    _observers.Clear();
                    break;
                }
            }
        }
    }
}
