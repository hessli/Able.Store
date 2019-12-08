using Able.Store.Infrastructure.ADO;
using Able.Store.InfrsturctureProvider.Domain.Connections;
using System.Text;

namespace Able.Store.InfrsturctureProvider.Repository
{
    public class ProviderFactoryRepository : IProviderFactoryRepository
    {

        public ProviderFactory GetKdBridProviderConnection()
        {
            StringBuilder sqlBuilder = new StringBuilder("SELECT* FROM cfg_provider inner join cfg_provider_kdbird ");
            sqlBuilder.AppendLine(" on   cfg_provider.id = cfg_provider_kdbird.provider_id");
            sqlBuilder.AppendLine(" where cfg_provider.record_state = 1 and cfg_provider_kdbird.record_state = 1");
            ProviderFactory results = null;
            using (var dataReader = DbHelperSQL.ExecuteReader(sqlBuilder.ToString()))
            {
                if (dataReader.HasRows)
                {
                    results = new ProviderFactory();
                    results.Remark = dataReader["remark"].ToString();
                    results.Provider = (ProviderType)int.Parse(dataReader["provider_type"].ToString());
                }
                while (dataReader.Read())
                {

                    var appKey = dataReader["appkey"] == null ? "" : dataReader["appkey"].ToString();
                    var eBusinessId = dataReader["e_business_id"] == null ? string.Empty : dataReader["e_business_id"].ToString();
                    var providerName = dataReader["provider_name"] == null ? string.Empty : dataReader["provider_name"].ToString();
                    var host = dataReader["host"] == null ? string.Empty : dataReader["host"].ToString();

                    results.KDBridConnections.Add(new KdBridConnect
                    {
                        AppKey = appKey,
                        EBusinessId = eBusinessId,
                        Host = host,
                        ProviderName = providerName
                    });
                }
                return results;
            }
        }
    }
}
