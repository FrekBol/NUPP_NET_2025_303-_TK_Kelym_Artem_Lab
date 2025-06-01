using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model
{
    using System;
    public class Member
    {
        // конструктор за замовчуванням
        public Member()
        {
            Id = Guid.NewGuid();                 // генеруємо унікальний ідентифікатор
            BorrowedItems = new List<LibraryItem>();
        }

        // інший конструктор із параметрами
        public Member(Guid id, string fullName, DateTime membershipDate)
        {
            Id = id;
            FullName = fullName;
            MembershipDate = membershipDate;
            BorrowedItems = new List<LibraryItem>();
        }

        // властивості
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime MembershipDate { get; set; }
        public List<LibraryItem> BorrowedItems { get; set; }

        // приклад події: коли член повертає книгу
        public event Action<LibraryItem> ItemReturned;   // делегат Action

        // метод для позичання предмета
        public void BorrowItem(LibraryItem item)
        {
            if (item != null)
            {
                BorrowedItems.Add(item);
                Console.WriteLine($"{FullName} borrowed: {item.Title}");
            }
        }

        // метод для повернення предмета (спрацьовуватиме подія)
        public void ReturnItem(LibraryItem item)
        {
            if (BorrowedItems.Remove(item))
            {
                ItemReturned?.Invoke(item);   // викликаємо подію, якщо є підписники
            }
        }
    }
}

