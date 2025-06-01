using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model
{
    using System;
    using System.Collections.Generic;

   
    
        /// <summary>
        /// Клас Author (автор книг/журналів)
        /// </summary>
        public class Author
        {
            // конструктор
            public Author(Guid id, string name, DateTime dateOfBirth)
            {
                Id = id;
                Name = name;
                DateOfBirth = dateOfBirth;
                BooksWritten = new List<Book>();     // ініціалізуємо колекцію
            }

            // властивості
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
            public List<Book> BooksWritten { get; set; }

            // приклад методу
            public void AddBook(Book book)
            {
                if (book != null)
                {
                    BooksWritten.Add(book);
                }
            }

            // статичний метод (наприклад, кількість авторів – можна лише демонстраційно)
            public static void PrintAuthorCount(List<Author> authors)
            {
                Console.WriteLine($"Total authors in list: {authors.Count}");
            }
        }
    }

