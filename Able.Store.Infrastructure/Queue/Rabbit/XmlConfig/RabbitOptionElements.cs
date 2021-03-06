﻿using System.Configuration;

namespace Able.Store.Infrastructure.Queue.Rabbit.XmlConfig
{
    public class RabbitOptionElements : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RabbitOptionElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RabbitOptionElement)element).TagName;
        }
    }
}
