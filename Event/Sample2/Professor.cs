using System;

namespace Event.Sample2
{
    /// <summary>
    /// Профессор получает уведомление от ректора и уведомляет студентов
    /// </summary>
    public class Professor
    {
        protected string _name;
        public string Name
        {
            get => _name;
        }

        public Professor()
        {
        }

        public Professor(string name) : this()
        {
            _name = name;
        }

        /// <summary>
        /// Профессор изменил оценки
        /// </summary>
        /// <param name="id">Индетификатор студента</param>
        public void ChangeMark(Guid id)
        {
            var randomMark = new Random().Next(0, 10);
            var point = (MarkEnum)randomMark;
            OnMarkChanged(new ProfessorMarkEventArgs((int)point, id));
        }

        /// <summary>
        /// Подписка на уведомление от ректора
        /// </summary>
        /// <param name="rector">Ректор</param>
        public void Subscribe(Rector rector)
        {
            rector.TimeTableChanged += Rector_TimeTableChanged;
        }

        /// <summary>
        /// Обработчки события, когда ректор уведомил об изменении расписания
        /// </summary>
        /// <param name="sender">В данном случае источник события - Ректор</param>
        /// <param name="e">Данные в уведомлении</param>
        private void Rector_TimeTableChanged(object sender, EventArgs e)
        {
            var rector = sender as Rector;

            Console.WriteLine(string.Format("Проффесор {0} уведомил(а) об изменении расписании", rector == null ? null : rector.Name));
        }

        protected virtual void OnMarkChanged(ProfessorMarkEventArgs me)
        {
            MarksChanged?.Invoke(this, me);
        }

        /// <summary>
        /// Событие, которое посылает профессор - изменение оценок 
        /// студентов в электронных зачётках
        /// </summary>
        public event EventHandler<ProfessorMarkEventArgs> MarksChanged;
    }

    /// <summary>
    /// Класс, которые хранит в себе данные об оценке студента,
    /// которую поменял профессор
    /// </summary>
    public class ProfessorMarkEventArgs : EventArgs
    {
        public int Mark { get; }
        public Guid Id { get; }

        public ProfessorMarkEventArgs() { Id = Guid.NewGuid(); }

        public ProfessorMarkEventArgs(int mark, Guid id)
        {
            Mark = mark;
            Id = id;
        }
    }

    public enum MarkEnum : int
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10
    }
}
