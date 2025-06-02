

namespace ClassLibrary.Model
{
    using System;
    /// <summary>
    /// Клас Book успадковує від LibraryItem
    /// </summary>
    public class Book : LibraryItem
    {
        // конструктор: виклик базового конструктора (LibraryItem)
        public Book(Guid id, string title, int yearPublished, string isbn, int numberOfPages)
            : base(id, title, yearPublished)
        {
            ISBN = isbn;
            NumberOfPages = numberOfPages;
        }

        // властивості (не менше 3)
        public string ISBN { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }

        // приклад метода
        public void PrintBookInfo()
        {
            Console.WriteLine($"[Book] {Title} ({YearPublished}), ISBN: {ISBN}, Pages: {NumberOfPages}");
        }
    }
}
