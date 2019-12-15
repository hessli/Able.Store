using Able.Store.Infrastructure.ADO;
using Able.Store.Infrastructure.ConfigCenter;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisConnectDAO: AbstractDbSQL, IConfigurationSource
    {
        public RedisConnectDAO() : base("baseProvider")
        {

        }
        public IEnumerable<IConnectOptions> Load()
        {
            StringBuilder sqlBuilder = new StringBuilder("SELECT* FROM cfg_provider inner join cfg_provider_redis_connect");
            sqlBuilder.AppendLine(" on cfg_provider.id = cfg_provider_redis_connect.provider_id");
            sqlBuilder.AppendLine(" where cfg_provider.record_state = 1 and cfg_provider_redis_connect.record_state = 1");
            IList<IConnectOptions> results = new List<IConnectOptions>();
            using (var dataReader = ExecuteReader(sqlBuilder.ToString()))
            {
                while (dataReader.Read())
                {
                    var item = new RedisConnectOptions
                    {

                        AbortOnConnectFail = dataReader["abort_on_connect_fail"] == null ? false
                            : System.Convert.ToBoolean(dataReader["abort_on_connect_fail"].ToString()),
                        Account = dataReader["account"] == null ? string.Empty : dataReader["account"].ToString(),
                        AllowAdmin = dataReader["allow_admin"] == null ? false : System.Convert.ToBoolean(dataReader["allow_admin"].ToString()),
                        ConnectRetry = dataReader["connect_retry"] == null ? 5 : int.Parse(dataReader["connect_retry"].ToString()),
                        Host = dataReader["host"] == null ? string.Empty : dataReader["host"].ToString(),
                        PassWord = dataReader["pass_word"] == null ? string.Empty : dataReader["pass_word"].ToString(),
                        Port = dataReader["port"] == null ? 0 : int.Parse(dataReader["port"].ToString()),
                        SyncTimeout = dataReader["sync_timeout"] == null ? 0 : int.Parse(dataReader["sync_timeout"].ToString()),
                        TagName = dataReader["tag_name"] == null ? string.Empty : dataReader["tag_name"].ToString()
                    };
                    results.Add(item);
                }
                return results;
            }
        }
    }
}
