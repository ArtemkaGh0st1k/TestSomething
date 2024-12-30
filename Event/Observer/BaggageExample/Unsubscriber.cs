using System;
using System.Collections.Generic;

namespace Event.Observer.BaggageExample
{
    internal class UnsubscriberBaggage<BaggageInfo> : IDisposable
    {
        private readonly ISet<IObserver<BaggageInfo>> _observers;
        private readonly IObserver<BaggageInfo> _observer;

        internal UnsubscriberBaggage(
            ISet<IObserver<BaggageInfo>> observers,
            IObserver<BaggageInfo> observer) => (_observers, _observer) = (observers, observer);
        public void Dispose() => _observers.Remove(_observer);
    }
}
