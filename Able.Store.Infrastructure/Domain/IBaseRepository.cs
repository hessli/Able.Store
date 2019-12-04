using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.Infrastructure.Domain
{
  public interface IBaseRepository<T>
    {
        void Add(T entity);

        void Remove(T entity);

    }
}
