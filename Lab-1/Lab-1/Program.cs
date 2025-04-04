using System;

namespace MilitaryAircraftHierarchy
{
    public class БойовийЛітак
    {
        public string Назва { get; set; }
        public double Швидкість { get; set; } 
        public int ДальністьПольоту { get; set; } 

        public virtual void ВиконатиЗавдання()
        {
            Console.WriteLine($"{Назва} виконує бойове завдання");
        }
    }

    public class РеактивнийЛітак : БойовийЛітак
    {
        public int КількістьДвигунів { get; set; }

        public void Зліт()
        {
            Console.WriteLine($"{Назва} злітає з реактивним прискоренням!");
        }
    }

    public class Винищувач : РеактивнийЛітак
    {
        public int КількістьРакет { get; set; }

        public void АтакуватиЦіль()
        {
            Console.WriteLine($"{Назва} атакує ціль з {КількістьРакет} ракетами");
            ВиконатиЗавдання();
        }

        public override void ВиконатиЗавдання()
        {
            Console.WriteLine("Перехоплення повітряних цілей");
        }
    }

    public class Бомбардувальник : БойовийЛітак
    {
        public string ТипБомб { get; set; }

        public void СкинутиБомби()
        {
            Console.WriteLine($"{Назва} скидає {ТипБомб} на ціль");
        }
    }

    public class Розвідник : БойовийЛітак
    {
        public bool МаєРадар { get; set; }

        public void ПровестиРозвідку()
        {
            Console.WriteLine($"{Назва} проводить радіоелектронну розвідку");
        }

        public override void ВиконатиЗавдання()
        {
            Console.WriteLine("Збір розвідувальних даних");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Винищувач f22 = new Винищувач()
            {
                Назва = "F-22 Raptor",
                Швидкість = 2410,
                ДальністьПольоту = 2960,
                КількістьДвигунів = 2,
                КількістьРакет = 8
            };

            Бомбардувальник b2 = new Бомбардувальник()
            {
                Назва = "B-2 Spirit",
                Швидкість = 1010,
                ДальністьПольоту = 11100,
                ТипБомб = "ядерні боєголовки"
            };

            Розвідник u2 = new Розвідник()
            {
                Назва = "U-2 Dragon Lady",
                Швидкість = 805,
                ДальністьПольоту = 11265,
                МаєРадар = true
            };

            f22.Зліт();
            f22.АтакуватиЦіль();

            b2.СкинутиБомби();
            b2.ВиконатиЗавдання();

            u2.ПровестиРозвідку();
            u2.ВиконатиЗавдання();

            Console.ReadKey();
        }
    }
}