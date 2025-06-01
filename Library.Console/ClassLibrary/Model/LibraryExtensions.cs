using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model
{
    public static class LibraryExtensions
    {
        // метод розширення: підраховує загальну кількість сторінок для списку книг (Book)
        public static int TotalPages(this IEnumerable<Book> books)
        {
            int sum = 0;
            foreach (var book in books)
            {
                sum += book.NumberOfPages;
            }
            return sum;
        }
    }
}

