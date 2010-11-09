using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Infrastructure
{
    public interface IRepository<T>
    {
        T FindBy(object id);
        IEnumerable<T> FindBy(DateTime from, DateTime to);
        IEnumerable<T> GetAll();

        void Add(T newItem);
        void Delete(T item);
        void Update(T item);
    }
}
