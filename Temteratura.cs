using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3_свой_тип
{
    public enum MeasureType { c, f, r, k };
    public class Temperatura
    {
        private double value;
        private MeasureType type;

        public Temperatura(double value, MeasureType type)
        {
            this.value = value;
            this.type = type;
        }
        public string Verbose()
        {
            string typeVerbose = "";
            switch (this.type)
            {
                case MeasureType.c:
                    typeVerbose = "цельсия";
                    break;
                case MeasureType.f:
                    typeVerbose = "фаренгейт";
                    break;
                case MeasureType.r:
                    typeVerbose = "ранкина";
                    break;
                case MeasureType.k:
                    typeVerbose = "кельвина";
                    break;
            }
            return String.Format("{0} {1}", Math.Round(this.value, 5), this.type);
        }
        public static Temperatura operator +(Temperatura instance, double number)
        {
            //// расчитываем новую значение
            //var newValue = instance.value + number;
            //// создаем новый экземпляр класса, с новый значением и типом как у меры, к которой число добавляем
            //var grad = new Temperatura(newValue, instance.type);
            //// возвращаем результат
            //return grad;
            return new Temperatura(instance.value + number, instance.type); 
        }
        public static Temperatura operator +(double number, Temperatura instance)
        {
            // вызываем с правильным порядком аргументов, то есть сначала длина потом число
            // для такого порядка мы определили оператор выше
            return instance + number;
        }
        // умножение
        public static Temperatura operator *(Temperatura instance, double number)
        {
            // мне лень по три строчки писать, поэтому я сокращаю код до одной строки
            return new Temperatura(instance.value * number, instance.type); ;
        }

        public static Temperatura operator *(double number, Temperatura instance)
        {
            return instance * number;
        }

        // вычитание
        public static Temperatura operator -(Temperatura instance, double number)
        {
            return new Temperatura(instance.value - number, instance.type); ;
        }

        public static Temperatura operator -(double number, Temperatura instance)
        {
            return instance - number;
        }

        // деление
        public static Temperatura operator /(Temperatura instance, double number)
        {
            return new Temperatura(instance.value / number, instance.type); ;
        }

        public static Temperatura operator /(double number, Temperatura instance)
        {
            return instance / number;
        }
        //public bool Compare(Temperatura instance1, Temperatura instance2)
        //{
        //    return instance1.value > instance2.value;
        //}

        //новая функция, возвращает тип Length, по имени To, на вход подается тип newType
        public Temperatura To(MeasureType newType)
        {
            // по умолчанию новое значение совпадает со старым
            var newValue = this.value;
            // если текущий тип -- это метр
            if (this.type == MeasureType.c)
            {
                // а теперь рассматриваем все другие ситуации
                switch (newType)
                {
                    // если конвертим в метр, то значение не меняем
                    case MeasureType.c:
                        newValue = this.value;
                        break;
                    // если в км.
                    case MeasureType.f:
                        newValue = (this.value * 1.8) + 32;
                        break;
                    // если в  а.е.
                    case MeasureType.r:
                        newValue = (this.value * 1.8) + 491.67;
                        break;
                    // если в парсек
                    case MeasureType.k:
                        newValue = this.value + 273.15;
                        break;
                }
            }
            else if (newType == MeasureType.c) // если новый тип: метр
            {
                switch (this.type) // а тут уже старый тип проверяем
                {
                    case MeasureType.c:
                        newValue = this.value;
                        break;
                    case MeasureType.f:
                        newValue = (this.value - 32) / 1.8 ; // кстати это то же код что и выше, только / заменили на *
                        break;
                    case MeasureType.r:
                        newValue = (this.value - 491.67) / 1.8; // и тут / на *
                        break;
                    case MeasureType.k:
                        newValue = this.value - 273.15; // и даже тут, просто / на *
                        break;
                }
               
            }
            else // то есть не в метр и не из метра
            {
                newValue = this.To(MeasureType.c).To(newType).value;
                // в принципе можно сразу написать 
                // return this.To(MeasureType.m).To(newType);
                // но хорошем тоном считается наличие всего одного return в функции
            }

            return new Temperatura(newValue, newType);
        }
        public static Temperatura operator +(Temperatura instance1, Temperatura instance2)
        {
            // то есть у текущей длине добавляем число 
            // полученное преобразованием значения второй длины в тип первой длины
            // так как у нас определен operator+(Length instance, double number)
            // то это сработает как ожидается
            return instance1 + instance2.To(instance1.type).value;
        }

        // вычитание двух длин
        public static Temperatura operator -(Temperatura instance1, Temperatura instance2)
        {
            // тут все тоже, только с минусом
            return instance1 - instance2.To(instance1.type).value;
        }
        public static Temperatura operator *(Temperatura instance1, Temperatura instance2)
        {
            // тут все тоже, только с с умножением
            return instance1 * instance2.To(instance1.type).value;
        }
        public static Temperatura operator /(Temperatura instance1, Temperatura instance2)
        {
            // тут все тоже, только с с умножением
            return instance1 / instance2.To(instance1.type).value;
        }

        //public static Temperatura operator >(Temperatura instance1, Temperatura instance2)
        //{
             
        //    return = Temperatura.Compare(instance1, instance2);
        //}
        //public static Temperatura operator <(Temperatura instance1, Temperatura instance2)
        //{
        //    // тут все тоже, только с с умножением
        //    return instance1 / instance2.To(instance1.type).value;
        //}


    }
}