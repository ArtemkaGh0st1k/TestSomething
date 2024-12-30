using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;


// TODO: Сделать для обобщенного типа Enum!
/* 
 * Пример решения проблемы с конвертации типов:
 * из String -> Enum
 */

namespace TestSomething.ToNIPI
{
    public enum TypeCar
    {
        [DescriptionAttribute("По умолчанию")]Default,
        [DescriptionAttribute("Автомобиль БМВ")]BMW,
        [DescriptionAttribute("Автомобиль Ауди")] Audi,
        [DescriptionAttribute("Автомобиль Мерседез")] Mercedez,
        [DescriptionAttribute("Автомобиль ЛАДА")] LADA,
        [DescriptionAttribute("Автомобиль Шкода")] Skoda
    }

    public class TestEnumConvertToString
    {
        public void ConverStrToEnum()
        {
            var DescriptionEnumValueDict = new Dictionary<string, TypeCar>();

            foreach (var typeCarStr in Enum.GetNames(typeof(TypeCar)))
            {
                try
                {

                    var valueAttributes = typeof(TypeCar).GetMember(typeCarStr)
                        .FirstOrDefault(m => m.DeclaringType == typeof(TypeCar))
                        .GetCustomAttributes(typeof(DescriptionAttribute), false);
                    var desc = ((DescriptionAttribute)valueAttributes[0]).Description;

                    DescriptionEnumValueDict.Add(desc, (TypeCar)Enum.Parse(typeof(TypeCar), typeCarStr, true));
                        
                }
                catch
                {
                    throw new Exception("Ошибка");
                }

            }

        }

        public static void Main(string[] args)
        {
            var testToEnum = new TestEnumConvertToString();
            testToEnum.ConverStrToEnum();
            Console.ReadLine();

        }
    }

   
}
