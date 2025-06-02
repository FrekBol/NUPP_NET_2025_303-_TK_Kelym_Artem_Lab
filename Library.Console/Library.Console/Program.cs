
using System.Collections.Generic;
using ClassLibrary.Model;
using ClassLibrary.Services;

namespace Library.Console
{
    using System;
    using ClassLibrary.Model;
    class Library

    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрація роботи генеричного CRUD-сервісу ===");

            // створюємо CRUD-сервіси для Book, Author та Member
            var bookService = new InMemoryCrudService<Book>();
            var authorService = new InMemoryCrudService<Author>();
            var memberService = new InMemoryCrudService<Member>();

            // ========================
            // 1. Додаємо кілька авторів
            // ========================
            var author1 = new Author(Guid.NewGuid(), "Leo Tolstoy", new DateTime(1828, 9, 9));
            var author2 = new Author(Guid.NewGuid(), "Fyodor Dostoevsky", new DateTime(1821, 11, 11));
            authorService.Create(author1);
            authorService.Create(author2);

            Console.WriteLine("Додано авторів:");
            foreach (var a in authorService.ReadAll())
            {
                Console.WriteLine($" - {a.Name} (ID: {a.Id})");
            }
            Console.WriteLine();

            // ========================
            // 2. Додаємо кілька книг
            // ========================
            var book1 = new Book(Guid.NewGuid(), "War and Peace", 1869, "978-0140447934", 1225)
            {
                Publisher = "Penguin Classics"
            };
            var book2 = new Book(Guid.NewGuid(), "Crime and Punishment", 1866, "978-0143058144", 671)
            {
                Publisher = "Vintage Classics"
            };

            bookService.Create(book1);
            bookService.Create(book2);

            Console.WriteLine("Додано книги:");
            foreach (var b in bookService.ReadAll())
            {
                b.PrintBookInfo();
            }
            Console.WriteLine();

            // ========================
            // 3. Демонструємо роботу методу розширення TotalPages
            // ========================
            Console.WriteLine($"Загальна кількість сторінок книг: {bookService.ReadAll().TotalPages()}");
            Console.WriteLine();

            // ========================
            // 4. Додаємо кілька учасників бібліотеки (Member)
            // ========================
            var member1 = new Member(Guid.NewGuid(), "Ivan Petrov", DateTime.Now.AddYears(-1));
            var member2 = new Member(Guid.NewGuid(), "Olga Sergeeva", DateTime.Now.AddMonths(-6));
            memberService.Create(member1);
            memberService.Create(member2);

            Console.WriteLine("Додано учасників:");
            foreach (var m in memberService.ReadAll())
            {
                Console.WriteLine($" - {m.FullName} (ID: {m.Id}, членство з {m.MembershipDate:d})");
            }
            Console.WriteLine();

            // ========================
            // 5. Демонструємо позичання і повернення предмета, підписка на подію
            // ========================
            member1.ItemReturned += item =>
            {
                Console.WriteLine($"[{member1.FullName}] повернув(ла) предмет: {item.Title}");
            };

            member1.BorrowItem(book1);    // Ivan Petrov позичає книгу War and Peace
            member1.ReturnItem(book1);    // Ivan Petrov повертає книгу → спрацьовує подія
            Console.WriteLine();

            // ========================
            // 6. Демонструємо Update і Remove у CRUD-сервісі
            // ========================
            // оновимо назву книги book2
            var bookToUpdate = bookService.Read(book2.Id);
            bookToUpdate.Title = "Преступление и наказание (Crime and Punishment)";
            bookService.Update(bookToUpdate);

            Console.WriteLine("Після оновлення назви книги:");
            foreach (var b in bookService.ReadAll())
            {
                b.PrintBookInfo();
            }
            Console.WriteLine();
            // видалимо book1
            bookService.Remove(book1);
            Console.WriteLine("Після видалення першої книги:");
            foreach (var b in bookService.ReadAll())
            {
                b.PrintBookInfo();
            }
            Console.WriteLine();

            Console.WriteLine("\n=== Кінець демонстрації ===");
            Console.ReadLine();
        }
    }
}