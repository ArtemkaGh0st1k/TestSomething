using System;

namespace Event.Sample2
{
    /// <summary>
    /// Ректор уведомляет профессоров и студентов
    /// </summary>
    public class Rector
    {
        protected string _name;
        public string Name
        {
            get => _name;
        }

        public Rector()
        {

        }

        public Rector(string name) : this()
        {
            _name = name;
        }

        /// <summary>
        /// Ректор изменил расписание
        /// </summary>
        public void ChangeTimeTable()
        {
            OnChangeTimeTable(EventArgs.Empty);
        }

        protected virtual void OnChangeTimeTable(EventArgs e)
        {
            TimeTableChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Событие, которое посылает ректор - это простое уведомление
        /// без каких-либо данных в событии
        /// </summary>
        public event EventHandler TimeTableChanged;
    }
}
