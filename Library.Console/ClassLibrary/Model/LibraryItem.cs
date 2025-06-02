using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model
{
   
    
        /// <summary>
        /// Базовий абстрактний клас для будь-якого елементу бібліотеки
        /// </summary>
        public abstract class LibraryItem
        {
            // конструктор
            protected LibraryItem(Guid id, string title, int yearPublished)
            {
                Id = id;
                Title = title;
                YearPublished = yearPublished;
            }

            // властивості (щонайменше 3)
            public Guid Id { get; set; }
            public string Title { get; set; }
            public int YearPublished { get; set; }
        }
    
}