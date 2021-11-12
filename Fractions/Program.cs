using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            Drob a = new Drob(4, -8);//создание объекта класса Drob
            Drob b = new Drob(2, 5);//создание объекта класса Drob
            Drob z = new Drob(81, 17, 21);
            Drob c;
            Console.WriteLine("Представление в виде обыкновенной дроби:" + a.ToString() + " " + b.ToString());
            Console.Write("Введите знак операции: ");
            string Znak = Console.ReadLine();
            string reznak = "+";
            Drob d = new Drob(4, 7);
            Console.WriteLine("Вывод индексов....");
            Console.WriteLine($"Индекс 0: {d[0]}");
            Console.WriteLine($"Индекс 1: {d[1]}");
            Console.WriteLine("Ивенты работают!");
            a.Change += FractionChange;
            a.Numinator_2 = 2;
            a.Denominator_2 = 5;
            switch (Znak)
            {
                case "+":
                    c = a + b;
                    Console.WriteLine("Проверка на сложение: " + a.ToString() + "+" + b.ToString() + "=" + c.ToString());
                    if ((c.Numinator_2 < 0))
                    {
                        reznak = "-";
                    }
                    Console.WriteLine("Знак дроби: " + reznak);
                    Console.WriteLine("Десятичная дробь: " + c.ToDouble());
                    break;
                case "-":
                    c = a - b;
                    Console.WriteLine("Проверка на вычитание: " + a.ToString() + "-" + b.ToString() + "=" + c.ToString());
                    if ((c.Numinator_2 < 0))
                    {
                        reznak = "-";
                    }
                    Console.WriteLine("Знак дроби: " + reznak);
                    Console.WriteLine("Десятичная дробь: " + c.ToDouble());
                    break;
                case "*":
                    c = a * b;
                    Console.WriteLine("Проверка на умножение: " + a.ToString() + "*" + b.ToString() + "=" + c.ToString());
                    if ((c.Numinator_2 < 0))
                    {
                        reznak = "-";
                    }
                    Console.WriteLine("Знак дроби: " + reznak);
                    Console.WriteLine("Десятичная дробь: " + c.ToDouble());
                    break;
                case "/":
                    c = a / b;
                    Console.WriteLine("Проверка на деление: " + a.ToString() + "/" + b.ToString() + "=" + c.ToString());
                    if ((c.Numinator_2 < 0))
                    {
                        reznak = "-";
                    }
                    Console.WriteLine("Знак дроби: " + reznak);
                    Console.WriteLine("Десятичная дробь: " + c.ToDouble());
                    break;
                default:
                    Console.WriteLine("Неизвестная операция(");
                    break;
            }
            Console.ReadKey();
        }
        public static void FractionChange(Drob fraction, int num)
        {
            Console.WriteLine("Урааааа");
        }
    }
    class Drob//Описание класса Drob
    {
        private int numerator = 0;
        private int denominator = 0;

        public Drob(int c, int z)
        {
            this.numerator = c;
            this.denominator = z;

        }
        public Drob(int c)
        {
            this.numerator = c;
            this.denominator = 1;
        }
        public Drob(int celoe, int c, int z)
        {
            this.numerator = celoe * z + c;
            this.denominator = z;
        }
        public char Sign { get; set; }
        public delegate void FractionChangeDelegate(Drob fraction, int num);

        public event FractionChangeDelegate Change;
        public int Numinator_2
        {
            get
            {
                return numerator;
            }

            set
            {
                // 4th task - call the event
                if (Change != null)
                {
                    Change(this, value);
                }
                numerator = value;
            }
        }
        public int Denominator_2
        {
            get
            {
                return denominator;
            }

            set
            {
                // 4th task - call the event
                if (Change != null)
                    Change(this, value);

                denominator = value;
            }
        }
        public double ToDouble()
        {
            return (double)(numerator) / denominator;
        }

        public override string ToString()//cтроковое представление
        {
            return "(" + numerator.ToString() + "/" + denominator.ToString() + ")";
        }

        public static Drob operator +(Drob a, Drob b)//сложение дробей
        {
            Drob l = new Drob(1, 1);//создание и инициализация новой дроби
            l.numerator = (a.numerator * b.denominator + a.denominator * b.numerator);//числитель новой дроби
            l.denominator = a.denominator * b.denominator;//знаменатель новой дроби
            Drob.Reduction(l);//сокращаем дробь
            return l;//возвращаем результат

        }
        public static Drob operator -(Drob a, Drob b)//вычитание дробей
        {
            Drob l = new Drob(1, 1);//создание и инициализация новой дроби
            l.numerator = (a.numerator * b.denominator - a.denominator * b.numerator);//числитель новой дроби
            l.denominator = a.denominator * b.denominator;//знаменатель новой дроби
            Drob.Reduction(l);//сокращаем дробь
            return
            l;//возвращаем результат

        }
        public static Drob operator *(Drob a, Drob b)//вычитание дробей
        {
            Drob l = new Drob(1, 1);//создание и инициализация новой дроби
            l.numerator = (a.numerator * b.numerator);//числитель новой дроби
            l.denominator = a.denominator * b.denominator;//знаменатель новой дроби
            Drob.Reduction(l);//сокращаем дробь
            return l;//возвращаем результат

        }
        public static Drob operator /(Drob a, Drob b)//вычитание дробей
        {
            Drob l = new Drob(1, 1);//создание и инициализация новой дроби
            l.numerator = (a.numerator / b.numerator);//числитель новой дроби
            l.denominator = a.denominator / b.denominator;//знаменатель новой дроби
            Drob.Reduction(l);//сокращаем дробь
            return l;//возвращаем результат
        }
        //процедура по сокращению дроби
        public static Drob Reduction(Drob a)
        {
            double max = 0;
            //выбираем что больше числитель или знаменатель
            if (a.numerator > a.denominator)
                max = Math.Abs(a.denominator);// берем по модулю, что работало и с отрицательными
            else
                max = Math.Abs(a.denominator);//берем по модулю, что работало и с отрицательными
                                              //поиск от максимума до 2
            for (double i = max; i >= 2; i--)
            {
                //такого числа, поделив на которое бы делилось без
                //остатка и на числитель и на знаменатель
                if ((a.numerator % i == 0) & (a.denominator % i == 0))
                {
                    a.numerator = Convert.ToInt32(a.numerator / i);
                    a.denominator = Convert.ToInt32(a.denominator / i);
                }

            }
            //Определяемся со знаком
            //если в знаменателе минус, поднимаем его наверх
            if ((a.denominator < 0))
            {
                a.numerator = -1 * (a.numerator);
                a.denominator = Math.Abs(a.denominator);
            }
            return (a);//возращаем результат
        }
        public int this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return numerator;
                }

                if (index == 1)
                {
                    return denominator;
                }

                return -1;
            }
        }
    }
}