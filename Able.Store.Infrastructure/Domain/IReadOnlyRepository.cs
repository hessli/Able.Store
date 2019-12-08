using Able.Store.Infrastructure.Querying;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T> where T:IAggregateRoot
    {
        T GetSingle();

        IEnumerable<T> GetList(Query queryable);

        T GetFirstOrDefault(Query queryable);

        IEnumerable<T> GetList(Query queryable, string[] includes);

        T GetFirstOrderDefault(Query queryable,string [] includes);

        T GetSingle(string[] includes);

        T GetFirstById(int id);
    }
}
