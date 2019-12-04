using System.Collections.Generic;

namespace Able.Store.Infrastructure.Querying
{
    public class Query: IQuery
    {
        private IList<Query> _subQueries = new List<Query>();

        private IList<Criterion> _criteria = new List<Criterion>();
   
        public IEnumerable<Criterion> Criteria
        {
            get {
                return _criteria;
             }
        }


        public IEnumerable<Query> SubQueries {

            get {
                return _subQueries;
            }
        }
        public void AddSubQueries(Query subQuery)
        {

            this._subQueries.Add(subQuery);

        }

        public void AddCriteria(Criterion criterion)
        {
            this._criteria.Add(criterion);

        }

        //当前语句和子语句的操作符
        public QueryOperator QueryOperator { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public OrderByClause OrderByProperty { get; set; }
    }
}
