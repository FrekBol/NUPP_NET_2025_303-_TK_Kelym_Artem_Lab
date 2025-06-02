using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class InMemoryCrudService<T> : ICrudService<T> where T : class
    {
        // внутрішня колекція
        private readonly List<T> _items;

        // конструктор
        public InMemoryCrudService()
        {
            _items = new List<T>();
        }

        public void Create(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            _items.Add(element);
        }

        public T Read(Guid id)
        {
            // шукаємо елемент із властивістю Id == id
            var prop = typeof(T).GetProperty("Id");
            if (prop == null)
                throw new InvalidOperationException("Тип T не містить властивості Id.");

            return _items.FirstOrDefault(item =>
            {
                var value = prop.GetValue(item);
                return value != null && (Guid)value == id;
            });
        }

        public IEnumerable<T> ReadAll()
        {
            return _items.ToList();
        }

        public void Update(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var prop = typeof(T).GetProperty("Id");
            if (prop == null)
                throw new InvalidOperationException("Тип T не містить властивості Id.");

            var idValue = (Guid)prop.GetValue(element);
            var existing = Read(idValue);
            if (existing == null)
                throw new InvalidOperationException("Елемент для оновлення не знайдено.");

            // замінюємо обʼєкт у колекції
            var index = _items.IndexOf(existing);
            _items[index] = element;
        }

        public void Remove(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            _items.Remove(element);
        }
    }
}

