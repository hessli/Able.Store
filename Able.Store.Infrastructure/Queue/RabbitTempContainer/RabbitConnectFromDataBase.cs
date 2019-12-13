using Able.Store.Infrastructure.ADO;
using Able.Store.Infrastructure.ConfigCenter;
using Able.Store.Infrastructure.Queue.Rabbit;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.Infrastructure.Queue.RabbitTempContainer
{
    public class RabbitConnectFromDataBase :AbstractDbSQL, IConfigurationSource
    {
        public RabbitConnectFromDataBase():base("ProviderConnectionString")
        {

        }
        public IEnumerable<T> Load<T>() where T : class, IConnectOptions
        {
            StringBuilder sqlBuilder = new StringBuilder("SELECT* FROM cfg_provider inner join cfg_provider_rabbitmq_connect ");
            sqlBuilder.AppendLine(" on   cfg_provider.id = cfg_provider_rabbitmq_connect.provider_id");
            sqlBuilder.AppendLine(" where cfg_provider.record_state = 1 and cfg_provider_rabbitmq_connect.record_state = 1");
            IList<T> results = new List<T>();
            using (var dataReader = ExecuteReader(sqlBuilder.ToString()))
            {
                while (dataReader.Read())
                {
                    var item = new RabbitConnectOptions
                    { AutomaticRecoveryEnabled = dataReader["automatic_recovery_enabled"] == null ? true : System.Convert.ToBoolean(dataReader["automatic_recovery_enabled"].ToString()),
                        Account = dataReader["account"] == null ? string.Empty : dataReader["account"].ToString(),
                        Host = dataReader["host"] == null ? string.Empty : dataReader["host"].ToString(),
                        PassWord = dataReader["pass_word"] == null ? string.Empty : dataReader["pass_word"].ToString(),
                        Port = dataReader["port"] == null ? 0 : int.Parse(dataReader["port"].ToString()),
                        TagName = dataReader["tag_name"] == null ? string.Empty : dataReader["tag_name"].ToString(),
                        VirtualHost = dataReader["virtual_host"] == null ? string.Empty : dataReader["virtual_host"].ToString()
                    };
                    results.Add(item as T);
                }
                return results;
            }
        }
    }
}
