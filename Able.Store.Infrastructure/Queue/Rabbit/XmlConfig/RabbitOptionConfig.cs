using System.Configuration;
namespace Able.Store.Infrastructure.Queue.Rabbit.XmlConfig
{
    public class RabbitOptionConfig : ConfigurationSection
    {
       
        [ConfigurationProperty("options")]
        [ConfigurationCollection(typeof(RabbitOptionElements),AddItemName = "RabbitOptionElement")]
        public RabbitOptionElements Elements
        {
            get { return (RabbitOptionElements)this["options"]; }
            set { this["options"] = value; }
        }


    }
}
