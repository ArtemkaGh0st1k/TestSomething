using System;

namespace Event.Sample2.Messages
{
    /// <summary>
    /// Сообщение студентам от преподавателя
    /// </summary>
    public class MessageToStudentFromProfessor : IMessageToStudentFromProfessor
    {
        public DateTime StartLesson { get; }

        public DateTime EndLesson { get; }

        public string Subject { get; }
        public Guid IdMessage { get; }
        public string From { get; }

        public MessageToStudentFromProfessor()
        {
            IdMessage = Guid.NewGuid();
        }

        public MessageToStudentFromProfessor(DateTime startLesson, DateTime endLesson, string subject, string from) : this()
        {
            StartLesson = startLesson;
            EndLesson = endLesson;
            Subject = subject;
            From = from;
        }
    }
}
