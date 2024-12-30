using System;

namespace Event.Sample1
{
    public class Counter
    {
        private int _threshold;
        private int _total;

        public Counter(int passedThreshold)
        {
            _threshold = passedThreshold;
        }

        public void Add(int x)
        {
            _total += x;
            if (_total >= _threshold)
            {
                ThresholdReached?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AddWithEventArgs(int x)
        {
            _total += x;
            if (_total >= _threshold)
            {
                var args = new ThresholdReachedEventArgs()
                {
                    Threshold = _threshold,
                    TimeReached = DateTime.Now
                };
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReachedWithArgs;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Объявили событие не предоставляющее данные
        /// </summary>
        public event EventHandler ThresholdReached;

        /// <summary>
        /// Объявили событие, которое предоставляет данные
        /// </summary>
        public event EventHandler<ThresholdReachedEventArgs> ThresholdReachedWithArgs;
    }
}
