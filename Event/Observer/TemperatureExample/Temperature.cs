using System;

namespace Event.Observer.TemperatureExample
{
    /// <summary>
    /// Содержит информацию, которую отправляет поставищк
    /// </summary>
    public struct Temperature
    {
        private double _temperature;
        private DateTime _temperatureDate;

        public Temperature(double temperature, DateTime temperatureDate)
        {
            _temperature = temperature;
            _temperatureDate = temperatureDate;
        }

        public double Degress { get => _temperature; }
        public DateTime Date { get => _temperatureDate; }
    }
}
