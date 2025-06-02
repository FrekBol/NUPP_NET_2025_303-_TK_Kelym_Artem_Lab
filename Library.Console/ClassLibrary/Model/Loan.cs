using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model
{
    public class Loan
    {
        // конструктор
        public Loan(Guid id, Guid itemId, Guid memberId, DateTime loanDate)
        {
            Id = id;
            ItemId = itemId;
            MemberId = memberId;
            LoanDate = loanDate;
            ReturnDate = null;
        }

        // властивості
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }  // nullable, якщо ще не повернено

        // статичний метод для обчислення затримки повернення
        public static TimeSpan? CalculateDelay(DateTime actualReturnDate, DateTime dueDate)
        {
            if (actualReturnDate > dueDate)
            {
                return actualReturnDate - dueDate;
            }
            return null;
        }
    }
}
