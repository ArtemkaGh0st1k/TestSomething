using System;

namespace Event.Sample2
{
    /// <summary>
    /// Студенты получают уведомление от профессора и ректора
    /// </summary>
    public class Student
    {
        protected string _name;
        public string Name { get => _name; }

        protected Guid _id;
        public Guid Id { get => _id; }

        public Student()
        {
            _id = Guid.NewGuid();
        }

        public Student(string name) : this()
        {
            _name = name;
        }

        /// <summary>
        /// Подписка на уведомление от профессора
        /// </summary>
        /// <param name="proffessor"></param>
        public void Subscribe(Professor proffessor)
        {
            proffessor.MarksChanged += Proffessor_MarksChanged;
        }

        /// <summary>
        /// Обработчик события, когда профессор изменил оценку
        /// </summary>
        /// <param name="sender">Источник события в данном случае - Профессор</param>
        /// <param name="me">Данные, которые содержаться в событии (в данном случае оценка студента)</param>
        private void Proffessor_MarksChanged(object sender, ProfessorMarkEventArgs me)
        {
            var proffesor = sender as Professor;
            if (Equals(me.Id, _id))
                Console.WriteLine($"{proffesor.Name} сообщил студенту {_name} оценку {me.Mark}");
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
        /// Обработчик события, когда ректор уведомил об изменении расписания
        /// </summary>
        /// <param name="sender">Источник события в данном случае - Ректор</param>
        /// <param name="e">Данные, которые содержаться в событии (в данном случае оно пустое)</param>
        private void Rector_TimeTableChanged(object sender, EventArgs e)
        {
            var rector = sender as Rector;
            if (rector != null)
                Console.WriteLine($"{rector.Name} уведомил студента {_name}");
        }
    }
}
