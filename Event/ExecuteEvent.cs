using Event.Sample2;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace Event
{
    public class ExecuteEvent
    {

        /* 
         * В данном классе показан пример как работает событие.
         * Понятие событие идёт совместно с понятием делегата.
         * Событие - это сообщение(уведомление) посланное объектом о том, где что-то произошло.
         * Делегат - это тип, содержащий ссылку на метод
         *      - Делегат объявляется с сигнатурой, указывающей тип возвращаемого значения и параметры
         *      для методов, на которые он ссылается.
         *      - Таким образом, делегат эквивалентен указателю на строго типизированную функцию.
         *      
         *      Связь с событем (как делегат связан с событием?):
         *      - В контексте событий делегат - это посреднк между источником события и кодом,
         *      обрабатывающим событие.
         *      - Делегат связывается с событием за счёт включения типа делегата в объявление события.
         *      
         *      public delegate void DoSomething(object sender, InheritFromEventArgsOrEventArgs e)
         *      
         *      
         *      1)  public event DoSomething Event;
         *      2)  public event EventHander EventHander;
         *      3)  public event EventHander<T> EventHanderWithArgs;
         *      
         *      В примерах после ключевого слова event идёт название метода.
         *      В 1-м случае мы конкретно задали делегат и его сигнатуру.
         *      
         *      Во 2-м и 3-м случает мы указали уже существующие типы, предоставляемые System
         *      Во 2-м примере EventHandler предоставляет метод обработки события, не имеющих данных
         *      В 3-м примере EventHandler<T> предоставляет метод обработки события, которое имееет данные
         */


        public static void Main()
        {
            MessageBox.Show(string.Format("Текущая запускаемая сборка: {0}", Assembly.GetExecutingAssembly().GetName().Name),
                            "Уведомление",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            ObserverExample();
            Console.ReadLine();
        }

        private static void ExecuteEventMethod()
        {
            var rector = new Rector("Богатырёв");

            var professor = new Professor("Кузьмин");
            professor.Subscribe(rector);

            var names = new List<string>()
            {
                "Васильев",
                "Данилов",
                "Семенов",
                "Мотыгин",
                "Чернов",
                "Бурцев"
            };

            var students = new List<Student>();
            foreach (var name in names)
            {
                students.Add(new Student(name));
            }

            students.ForEach(
                x =>
                {
                    x.Subscribe(professor);
                    x.Subscribe(rector);
                });

            rector.ChangeTimeTable();
            students.ForEach(x =>
            {
                professor.ChangeMark(x.Id);
            });
        }

        private static void ObserverExample()
        {
            var studentNames = new List<string>()
            {
                "Васильев",
                "Данилов",
                "Семенов",
                "Мотыгин",
                "Чернов",
                "Бурцев"
            };
            var professorNames = new List<string>()
            {
                "Кузьмин",
                "Скиданов",
                "Логанова",
            };
            var rectorName = "Богатырёв";

            var rector = new RectorObservable(rectorName);

            var professors = new List<ProfessorObservableAndObserver>();
            professorNames.ForEach(name => professors.Add(new ProfessorObservableAndObserver(name)));

            var students = new List<StudentObserver>();
            studentNames.ForEach(name => students.Add(new StudentObserver(name)));

            professors.ForEach(p => rector.Subscribe(p)) ; // Ректора могут посылать уведомления профессорам
            students.ForEach(s => rector.Subscribe(s)); // Ректора могут посылать уведомления студентам

            professors.ForEach(p => p.SubscribeToRector(rector));   // Профессора получают уведомления от ректора
            professors[0].Subscribe(students[0]); professors[0].Subscribe(students[1]);     // Профессора могут посылать уведомления студентам
            professors[1].Subscribe(students[2]); professors[1].Subscribe(students[3]);     // За каждым профессором - 2 студента
            professors[2].Subscribe(students[4]); professors[2].Subscribe(students[5]);

            students.ForEach(s => s.SubscribeToRector(rector));     // Студенты могут получать уведомеления от ректора
            students[0].SubscribeToProfessor(professors[0]); students[1].SubscribeToProfessor(professors[0]);       // Каждый студент получает уведомления от закрепленного за ним профессора
            students[2].SubscribeToProfessor(professors[1]); students[3].SubscribeToProfessor(professors[1]);
            students[4].SubscribeToProfessor(professors[2]); students[5].SubscribeToProfessor(professors[2]);

            rector.PushProfessors();
            rector.PushStudents();

            professors.ForEach(p => p.PushStudents());



            /* 
             * Простой вариант
            var professor = new ProfessorObservableAndObserver("Кузьмин");
            var student = new StudentObserver("Васильев");

            rector.Subscribe(professor);
            rector.Subscribe(student);

            professor.SubscribeToRector(rector);
            professor.Subscribe(student);

            student.SubscribeToProfessor(professor);
            student.SubscribeToRector(rector);

            rector.PushProfessors();
            rector.PushStudents();

            professor.PushStudents();
            */
        }
    }
}
