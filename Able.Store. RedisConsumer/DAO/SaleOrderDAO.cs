using Able.Store.RedisConsumer.Domain.OrdersDomain;
using Dapper;
namespace Able.Store.RedisConsumer.DAO
{
   public class SaleOrderDAO
    {

        private string connectionStr = System.Configuration.ConfigurationManager.AppSettings["store"];
        public void UpdateState(Order order) {

            using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionStr))
            {
                connection.Execute("update oms_order set order_status=@Status where id=@Id", order);
            }
        }
        public Order GetOrder(int id)
        {
            using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionStr))
            {
               var order  =connection.QueryFirstOrDefault<Order>("Select * from  oms_order  where id=@id", id);
                return order;
            }
        }
    }
}
