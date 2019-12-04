using System.Configuration;
namespace Able.Store.Infrastructure.Queue.Rabbit.SourceConfig
{
    public class RabbitOptionConfig : ConfigurationSection
    {
        //public IConnectOptions LoadConfigOptions()
        //{
        //    var datas = ConfigurationBroker
        //        .GetConfigurationObject<string, IList<RabbitConnectOptions>>("rabbit");
          
        //    if (datas.Count > 0)
        //    {
        //        return  datas[0];
        //    }
        //    return null;
        //}
        [ConfigurationProperty("options")]
        [ConfigurationCollection(typeof(RabbitOptionElements),AddItemName = "RabbitOptionElement")]
        public RabbitOptionElements Elements
        {
            get { return (RabbitOptionElements)this["options"]; }
            set { this["options"] = value; }
        }


    }
}
