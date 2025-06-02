using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model
{
    using System;
    public class Magazine : LibraryItem
    {
        // конструктор
        public Magazine(Guid id, string title, int yearPublished, int issueNumber, string editor)
            : base(id, title, yearPublished)
        {
            IssueNumber = issueNumber;
            Editor = editor;
        }

        // властивості
        public int IssueNumber { get; set; }
        public string Editor { get; set; }
        public int NumberOfPages { get; set; }

        // статичне поле (наприклад, лічильник журналів)
        public static int MagazineCounter;

        // статичний конструктор
        static Magazine()
        {
            // коли завантажується тип, ініціалізуємо лічильник
            MagazineCounter = 0;
        }

        // метод розширення (розміщу пізніше)
        // … (поки залишимо порожнім)

        // приклад статичного методу
        public static void PrintMagazineCount()
        {
            Console.WriteLine($"Total magazines created: {MagazineCounter}");
        }

        // створюємо перегрузку методу для інкременту лічильника
        public void IncrementCounter()
        {
            MagazineCounter++;
        }
    }
}

