using Event.Sample2.Messages;
using System;

namespace Event.Sample2
{
    public class StudentObserver : Student, IObserver<IMessage>
    {
        #region Fields

        private IDisposable _unsubscribeFromRector;
        private IDisposable _unsubscribeFromProfessor;
        private bool _isRectorFrom = false;
        private bool _isProfessorFrom = false;
        private string _messageFromRector;
        private string _messageFromProfessor;
        private string _fromRector;
        private string _fromProfessor;

        #endregion

        public string MessageFromRector { get => _messageFromRector; }
        public string MessageFromProfessor { get => _messageFromProfessor; }

        #region Constructor
        public StudentObserver()
        {

        }

        public StudentObserver(string name) : base(name)
        {

        }

        #endregion

        #region Релизация методов наблюдателя
        public void OnCompleted()
        {
            if (_isRectorFrom)
                Console.WriteLine("Студент {0} был уведомлен от ректора {1}\n", Name, _fromRector);
            if (_isProfessorFrom)
                Console.WriteLine("Студент {0} был уведомлен от профессора {1}\n", Name, _fromProfessor);

            _isRectorFrom = false;
            _isProfessorFrom = false;
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(IMessage value)
        {
            if (_isRectorFrom)
            {
                var message = (IMessageToStudentFromRector)value;
                _fromRector = message.From;
                _messageFromRector = string.Format("Студент {0} получил следующую информацию:\n" +
                                                    "Начало экзамена : {1} " +
                                                    "Будут ли изменения в сдаче экзаменов : {2} " +
                                                    "Рейтинг студента : {3} ",
                                                    Name,
                                                    message.DateExam,
                                                    message.IsChangeExam is true ? "Да " : "Нет ",
                                                    message.Raiting);
                Console.WriteLine($"{_messageFromRector}\n");
            }
            else if (_isProfessorFrom)
            {
                var message = (IMessageToStudentFromProfessor)value;
                _fromProfessor = message.From;
                _messageFromProfessor = string.Format("Студент {0} получил следующую информацию:\n" +
                                                      "Экзамен по {1} начнется в {2} и закончится {3}",
                                                      Name,
                                                      message.Subject, message.StartLesson, message.EndLesson);
                Console.WriteLine($"{_messageFromProfessor}\n");
            }
        }

        #endregion

        #region Подписка на уведомления от поставщика
        public void SubscribeToRector(IObservable<IMessage> provider)
        {
            _unsubscribeFromRector = provider.Subscribe(this);
            ((RectorObservable)provider).TimeTableChanged += StudentObserver_TimeTableChanged;
        }

        public void SubscribeToProfessor(IObservable<IMessage> provider)
        {
            _unsubscribeFromProfessor = provider.Subscribe(this);
            ((ProfessorObservableAndObserver)provider).MarksChanged += StudentObserver_MarksChanged;
        }

        #endregion

        #region Отписка от поставщика уведомлений
        public void UnSubscribeFromRector()
        {
            _unsubscribeFromRector.Dispose();
            _isRectorFrom = false;
        }

        public void UnSubscribeFromProfessor()
        {
            _unsubscribeFromProfessor.Dispose();
            _isProfessorFrom = false;
        }

        #endregion

        #region PrivateMethods/Handlers

        private void StudentObserver_MarksChanged(object sender, ProfessorMarkEventArgs e)
        {
            _isProfessorFrom = true;
        }
        private void StudentObserver_TimeTableChanged(object sender, EventArgs e)
        {
            _isRectorFrom = true;
        }

        #endregion
    }
}
