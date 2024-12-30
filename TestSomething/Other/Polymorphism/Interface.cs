using System.ComponentModel;

namespace TestSomething.Other.Polymorphism
{

    #region Вложенные друг в друга интрефейсы

    public interface IInterfaceA : INotifyPropertyChanged
    {
        double PropertA { get; set; }
    }

    public interface IInterfaceB_A : IInterfaceA
    {
        double PropertyB { get; set; }
    }

    public interface IInterfaceC_B_A : IInterfaceB_A
    {
        double PropertyC { get; set; }
    }

    #endregion

    #region Независимые друг от друга интрефейсы

    public interface IInterfaceA1
    {
        double PropertyA1 { get; set; }
    }

    public interface IInterfaceB1
    {
        double PropertyB1 { get; set; }
    }

    public interface IInterfaceC1
    {
        double PropertyC1 { get; set; }
    }

    #endregion
}
