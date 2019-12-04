using System.Collections.Generic;

namespace Able.Store.Infrastructure.Querying
{
    public interface IQuery
    {
        IEnumerable<Criterion> Criteria { get; }

        IEnumerable<Query> SubQueries { get; }

        void AddSubQueries(Query subQuery);
    }
}
