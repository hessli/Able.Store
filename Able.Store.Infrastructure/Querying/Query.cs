using System.Collections.Generic;

namespace Able.Store.Infrastructure.Querying
{
    public class Query
    {
        private IList<Query> _subQueries;
        private IList<Criterion> _criteria;

        public Query()
        {
            _criteria = new List<Criterion>();
            _subQueries=new List<Query>();
        }

        public void AddSubQuery(Query subQuery)
        {
            _subQueries.Add(subQuery);
        }
        public IEnumerable<Query> SubQueries
        {
            get { return _subQueries; }
        }

        public IEnumerable<Criterion> Criteria
        {
            get
            {
                return _criteria;
            }
        }


        public void AddCriteria(Criterion Criterion)
        {
            _criteria.Add(Criterion);
        }
    
        public QueryOperator QueryOperator { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public OrderByClause OrderByProperty { get; set; }
    }
}
