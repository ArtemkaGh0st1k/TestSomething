using Event.Sample2.Messages;
using System;
using System.Collections.Generic;

namespace Event.Sample2
{
    /// <summary>
    /// Профессор выступает в роли наблюдателя - получает уведомления от ректора 
    /// и в то же время поставщиком - отправляет уведомления студентам
    /// </summary>
    public class ProfessorObservableAndObserver : Professor, IObservable<IMessage>, IObserver<IMessage>
    {
        private List<IObserver<IMessage>> _studentsObservers;
        private IDisposable _unsubscriberFromRector;
        private string _messageFromRector;
        private string _from;

        public string MessageFromRector { get => _messageFromRector; }

        public ProfessorObservableAndObserver()
        {
            _studentsObservers = new List<IObserver<IMessage>>();
        }

        public ProfessorObservableAndObserver(string name) : this()
        {
            _name = name;
        }

        public void OnCompleted()
        {
            Console.WriteLine("Профессор {0} был уведомлен о сообщении ректора {1}", Name, _from);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnNext(IMessage value)
        {
            var message = (IMessageToProfessorFromRector)value;
            _from = message.From;
            _messageFromRector = string.Format("Дата получения повышенной зарплаты {0}\n" +
                                                "Повышение {1}\n" +
                                                "Запрлата повысится на {2}",
                                                 message.DateCrediting,
                                                 message.IsPromotion is true ? "Повысят!" : "Не повысят!",
                                                 message.SalaryDiff);
            Console.WriteLine($"{_messageFromRector}\n");
        }

        private class Unsubscribe : IDisposable
        {
            private List<IObserver<IMessage>> _observers;
            private IObserver<IMessage> _observer;
            public Unsubscribe(List<IObserver<IMessage>> observers, IObserver<IMessage> observer)
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
            if (!_studentsObservers.Contains(observer))
                _studentsObservers.Add(observer);

            return new Unsubscribe(_studentsObservers, observer);
        }

        public void SubscribeToRector(IObservable<IMessage> provider) =>
            _unsubscriberFromRector = provider.Subscribe(this);

        public void UnSubscribeFromRector() => 
            _unsubscriberFromRector.Dispose();

        /// <summary>
        /// Разослать уведомление студентам
        /// </summary>
        public void PushStudents()
        {
            base.OnMarkChanged(new ProfessorMarkEventArgs());

            var messages = new List<MessageToStudentFromProfessor>();

            _studentsObservers.ForEach(x =>
            {
                messages.Add(new MessageToStudentFromProfessor(DateTime.Now, DateTime.Now, "История", Name));
            });

            var count = 0;
            foreach (StudentObserver studentObserver in _studentsObservers)
            {
                studentObserver.OnNext(messages[count++]);
                studentObserver.OnCompleted();
            }
        }
    }
}
