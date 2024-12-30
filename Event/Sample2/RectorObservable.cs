using Event.Sample2.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Event.Sample2
{
    /*
     * Пусть ректор посылает сообщение профессорам и студентам.
     * Для профессоров:
     *      - Изменения по зарплате (повышение на какую сумму double)
     *      - Будет ли повышения у преподавателя (true/false)
     *      - Когда придёт новая повышенная зарплата (DateTime)
     * 
     * Для студентов:
     *      - Когда будет вывешен список дат сдачи экзаменов и зачётов (DateTime)
     *      - Будет ли изменения в сдачи экзаменов (true/false)
     *      - Рейтинг студента (Enum Raiting)
     */

    /// <summary>
    /// Ректор выступает в роли поставщиком уведомления для студентов и преподавтелей
    /// </summary>
    public class RectorObservable : Rector, IObservable<IMessage>
    {
        private List<IObserver<IMessage>> _observers;
        public RectorObservable()
        {
            _observers = new List<IObserver<IMessage>>();
        }

        public RectorObservable(string name) : this()
        {
            _name = name;
        }

        private class Unsubcribe : IDisposable
        {
            private List<IObserver<IMessage>> _observers;
            private IObserver<IMessage> _observer;

            public Unsubcribe(List<IObserver<IMessage>> observers, IObserver<IMessage> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers != null)
                    _observers.Remove(_observer);
            }
        }

        public IDisposable Subscribe(IObserver<IMessage> observer)
        {
            if (!(_observers.Contains(observer)))
                _observers.Add(observer);

            return new Unsubcribe(_observers, observer);
        }

        /// <summary>
        /// Разослать уведомление профессорам
        /// </summary>
        public void PushProfessors()
        {
            base.OnChangeTimeTable(EventArgs.Empty);

            var messages = new List<MessageToProfessorFromRector>();

            var deafaultSalary = 10d;
            _observers.ForEach(x =>
            {
                messages.Add(new MessageToProfessorFromRector(deafaultSalary++, false, DateTime.Now, Name));
            });

            var count = 0;
            foreach (ProfessorObservableAndObserver professorObservableAndObserver in _observers.OfType<ProfessorObservableAndObserver>())
            {
                professorObservableAndObserver.OnNext(messages[count++]);
                professorObservableAndObserver.OnCompleted();
            }
        }

        /// <summary>
        /// Разослать уведомление студентам
        /// </summary>
        public void PushStudents()
        {
            base.OnChangeTimeTable(EventArgs.Empty);

            var messages = new List<MessageToStudentFromRector>();

            _observers.ForEach(x =>
            {
                messages.Add(new MessageToStudentFromRector(DateTime.Now, false, null, Name));
            });

            var count = 0;
            foreach (StudentObserver studentObserver in _observers.OfType<StudentObserver>())
            {
                studentObserver.OnNext(messages[count++]);
                studentObserver.OnCompleted();
            }
        }
    }
}
