using System.ComponentModel;
using System.Diagnostics;

namespace TestSomething.Other.Polymorphism
{
    public class Polymorphism : IInterfaceC_B_A
    {
        private double _propertyA = 1d;
        private double _propertyB = 2d;
        private double _propertyC = 3d;

        public double PropertyC
        {
            get => _propertyC;
            set
            {
                if (Equals(_propertyC, value)) return;
                OnPropertyChanged("PropertyC");
                Debug.WriteLine("PropertyC Changed");
            }
        }
        public double PropertyB
        {
            get => _propertyB;
            set
            {
                if (Equals(_propertyB, value)) return;
                OnPropertyChanged("PropertyB");
                Debug.WriteLine("PropertyB Changed");
            }
        }
        public double PropertA
        {
            get => _propertyA;
            set
            {
                if (Equals(_propertyA, value)) return;
                OnPropertyChanged("PropertyA");
                Debug.WriteLine("PropertyA Changed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Polymorphism1 : IInterfaceA1, IInterfaceB1, IInterfaceC1, INotifyPropertyChanged
    {
        private double _propertyA1 = 1d;
        private double _propertyB1 = 2d;
        private double _propertyC1 = 3d;
        public double PropertyC1
        {
            get => _propertyC1;
            set
            {
                if (Equals(_propertyC1, value)) return;
                OnPropertyChanged("PropertyC1");
                Debug.WriteLine("PropertyC1 Changed");
            }
        }
        public double PropertyB1
        {
            get => _propertyB1;
            set
            {
                if (Equals(_propertyB1, value)) return;
                OnPropertyChanged("PropertyB1");
                Debug.WriteLine("PropertyB1 Changed");
            }
        }
        public double PropertyA1
        {
            get => _propertyA1;
            set
            {
                if (Equals(_propertyA1, value)) return;
                OnPropertyChanged("PropertyA1");
                Debug.WriteLine("PropertyA1 Changed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class InitializeClass
    {
        public IInterfaceA InterfaceA { get; set; }
        public IInterfaceB_A InterfaceB_A { get; set; }
        public IInterfaceC_B_A InterfaceC_B_A { get; set; }

        public IInterfaceA1 InterfaceA1 { get; set; }
        public IInterfaceB1 InterfaceB1 { get; set; }
        public IInterfaceC1 InterfaceC1 { get; set; }

        public InitializeClass()
        {
            InterfaceA = new Polymorphism();
            InterfaceB_A = new Polymorphism();
            InterfaceC_B_A = new Polymorphism();

            InterfaceA1 = new Polymorphism1();
            InterfaceB1 = new Polymorphism1();
            InterfaceC1 = new Polymorphism1();
        }

        public void Cast()
        {
            var typeInterfaceA = typeof(IInterfaceA);
            var typeInterfaceB_A = typeof(IInterfaceB_A);
            var typeIntefaceC_B_A = typeof(IInterfaceC_B_A);

            InterfaceA.PropertA = 10d;

            var checkAtoB_A = typeInterfaceA == typeInterfaceB_A;
            var checkAtoC_B_A = typeInterfaceA == typeIntefaceC_B_A;
            var checkB_AtoA = typeInterfaceB_A == typeInterfaceA;
            var checkB_AtoC_B_A = typeInterfaceB_A == typeIntefaceC_B_A;
            var checkC_B_AtoA = typeIntefaceC_B_A == typeInterfaceA;
            var checkC_B_AtoB_A = typeIntefaceC_B_A == typeInterfaceB_A;

            return;
        }
    }

    public class ExecuteClass
    {
        public static void ExecutePolymorhism()
        {
            // Без явного указания типа создаёт экземпляр данного класса
            var poly = new InitializeClass();
            poly.Cast();

            // Т.к класс Polymorphism реализует интерфейс IInterfaceC_B_A, но этот интрефейс реализует
            // IInterfaceB_A, и тот в свою очерель реализует IInterfaceA т.е IInterfaceC_B_A - в него вложенные два других интерфейса 
            // мы можем делать такие инициализации
            IInterfaceA poly_A = new Polymorphism();
            IInterfaceB_A poly_B_A = new Polymorphism();
            IInterfaceC_B_A poly_C_B_A = new Polymorphism();

            // Аналогчно и для этого случая, только здесь интерфейсы независимы друг от друга
            IInterfaceA1 poly_A1 = new Polymorphism1();
            IInterfaceB1 poly_B1 = new Polymorphism1();
            IInterfaceC1 poly_C1 = new Polymorphism1();


            /* 
             * Интрефейс является ссылочным типом; его нельзя создать с помщью конструктора
             * Пусть у нас идёт иерархия наследования интерфейсов: IC_B_A <- I_B_A <- I_A, где <- в какую сторону идёт наследование (I_C_B_A содержит данные из I_B_A)
             * Родителем является I_A
             * Задали свойство в каждом из интрефейсов для наглядного примера
             * !!! В режиме Debug будет показываться данные, но возможно к ним не будет доступа.
             * Необходимо привести объект к соответсвующему типу, чтобы можно было обратится к нужному члену.
             */

            // Данный интерфейс будет содержать свойтства A и B, хотя в Debug'е будет показаны все свойства
            IInterfaceB_A interfaceB_A = poly.InterfaceC_B_A;
            
            // Данный интерфейс будер содержать только свойство A, хотя в Debug'е будет показаны все свойства
            IInterfaceA interfaceA = poly.InterfaceC_B_A;

            // Т.к родительский интерфейс (самый первый) имеет меньше всего данных в своем пространстве,
            // то необходимо привести его к необходимому типу (в данном случае интерфейсу)
            IInterfaceC_B_A interfaceC_B_To_C_B_A = (IInterfaceC_B_A)poly.InterfaceB_A; 
            IInterfaceC_B_A interfaceA_To_C_B_A = (IInterfaceC_B_A)poly.InterfaceA;
            IInterfaceB_A interfaceA_To_B_A = (IInterfaceB_A)poly.InterfaceA;


            // В данном случае нельзя  явно присвоить один тип интерфейса другому
            // Т.к все интерфейсы не имеет связи между собой, то здесь работает только такое приведение типа -
            // От одного класса, который реализует несколько независимых интрефейсов
            IInterfaceB1 interfaceB1 = (IInterfaceB1)poly.InterfaceA1;
            IInterfaceC1 interfaceC1 = (IInterfaceC1)poly.InterfaceA1;
            IInterfaceA1 interfaceA1 = (IInterfaceA1)poly.InterfaceC1;
            

            var exit = 0;
        }
    }
}
