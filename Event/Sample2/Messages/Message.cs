using System;

namespace Event.Sample2.Messages
{
    /// <summary>
    /// Сообщение студентам от ректора
    /// </summary>
    public interface IMessageToStudentFromRector : IMessage
    {
        DateTime DateExam { get; }
        bool IsChangeExam { get; }
        Raiting Raiting { get; }
    }

    /// <summary>
    /// Сообщение профессорам от ректора
    /// </summary>
    public interface IMessageToProfessorFromRector : IMessage
    {
        double SalaryDiff { get; }
        bool IsPromotion { get; }
        DateTime DateCrediting { get; }
    }

    /// <summary>
    /// Сообщение студентам от профессора
    /// </summary>
    public interface IMessageToStudentFromProfessor : IMessage
    {
        DateTime StartLesson { get; }
        DateTime EndLesson { get; }
        string Subject { get; }
    }

    public interface IMessage
    {
        Guid IdMessage { get; }
        string From { get; }
    }

    public class Message
    {
    }
}
