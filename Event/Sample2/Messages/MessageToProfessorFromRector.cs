using System;

namespace Event.Sample2.Messages
{
    /// <summary>
    /// Сообщение профессорам от ректора
    /// </summary>
    public class MessageToProfessorFromRector : IMessageToProfessorFromRector
    {
        public double SalaryDiff { get; }
        public bool IsPromotion { get; }
        public DateTime DateCrediting { get; }
        public Guid IdMessage { get; }
        public string From { get; }

        public MessageToProfessorFromRector()
        {
            IdMessage = Guid.NewGuid();
        }

        public MessageToProfessorFromRector(double salaryDiff, bool isPromotion, DateTime dateCrediting, string from) : this()
        {
            SalaryDiff = salaryDiff;
            IsPromotion = isPromotion;
            DateCrediting = dateCrediting;
            From = from;
        }
    }
}
