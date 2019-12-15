using Able.Store.Infrastructure.Dappers;
using Able.Store.RedisConsumer.Domain.OrdersDomain;
using Dapper;

namespace Able.Store.RedisConsumer.Start
{
    public class ColumnMapper
    {
        public static void SetMapper()
        {
            SqlMapper.SetTypeMap(typeof(Order), new ColumnAttributeTypeMapper<Order>());
        }
    }
}
