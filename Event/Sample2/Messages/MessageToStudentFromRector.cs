using System;

namespace Event.Sample2.Messages
{
    public enum Raiting : int
    {
        Worse = 1,
        Bad = 2,
        Middle = 3,
        Good = 4,
        Great = 5
    }

    /// <summary>
    /// Сообщение студентам от ректора
    /// </summary>
    public class MessageToStudentFromRector : IMessageToStudentFromRector
    {
        public DateTime DateExam { get; }
        public bool IsChangeExam { get; }
        public Raiting Raiting { get; }
        public Guid IdMessage { get; }
        public string From { get; }

        public MessageToStudentFromRector()
        {
            IdMessage = Guid.NewGuid();
        }

        public MessageToStudentFromRector(DateTime dateExam, bool isChangeExam, Raiting? raiting, string from) : this()
        {
            DateExam = dateExam;
            IsChangeExam = isChangeExam;
            Raiting = raiting ?? (Raiting)new Random().Next(1, 5);
            From = from;
        }
    }
}
