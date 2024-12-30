namespace Event.Observer.BaggageExample
{
    /// <summary>
    /// Предоставляет инфо о прибывающих рейсах
    /// </summary>
    public struct BaggageInfo
    {
        /// <summary>
        /// Номер рейса
        /// </summary>
        public int FlightNumber { get; set; }
        
        /// <summary>
        /// Аэропорт отправления
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Лента
        /// </summary>
        public int Carousel { get; set; }

        public BaggageInfo(int flightNumber, string from, int carousel)
        {
            FlightNumber = flightNumber;
            From = from;
            Carousel = carousel;
        }
    }
}
