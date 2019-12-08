using Able.Store.Infrastructure.ADO;
using Able.Store.Infrasturcture.Service.Interface.logistics;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.Infrasturcture.Service.Implementations.Logistics.KdBird
{
   public  class ConnectionFromDataBase
    {  
            public IList<ILogisticsConnectOption> Load() 
            {
                StringBuilder sqlBuilder = new StringBuilder("SELECT* FROM cfg_provider inner join cfg_provider_kdbird ");
                sqlBuilder.AppendLine(" on   cfg_provider.id = cfg_provider_kdbird.provider_id");
                sqlBuilder.AppendLine(" where cfg_provider.record_state = 1 and cfg_provider_kdbird.record_state = 1");
                IList<ILogisticsConnectOption> results = new List<ILogisticsConnectOption>();
                using (var dataReader = DbHelperSQL.ExecuteReader(sqlBuilder.ToString()))
                {
                    while (dataReader.Read())
                    {

                    var appKey = dataReader["appkey"] == null ? "" : dataReader["appkey"].ToString();
                    var  eBusinessId = dataReader["e_business_id"] ==  null ? string.Empty : dataReader["e_business_id"].ToString();
                    var tagName= dataReader["tag_name"] == null ? string.Empty : dataReader["tag_name"].ToString();
                    var url = dataReader["url"] == null ? string.Empty : dataReader["url"].ToString();
                    ILogisticsConnectOption item = new KdBirdConnectOption(appKey, url,eBusinessId, tagName);
                        results.Add(item );
                    }
                    return results;
                }
        }
    }
}
