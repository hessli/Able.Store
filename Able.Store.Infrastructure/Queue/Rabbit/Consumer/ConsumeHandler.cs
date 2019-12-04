using Able.Store.Infrastructure.Utils;
using System;
using System.Text;

namespace Able.Store.Infrastructure.Queue.Rabbit.Consumer
{
    public delegate void ConsumeHandle (ConsumeArgs data);
    public class ConsumeArgs  : EventArgs
    {
        public byte[] Data { get; private set; }
    
        public RabbitRequest  GetReqeust()
        {
            var dataStr = Encoding.UTF8.GetString(Data);
            var data = JsonPase.Deserialize<RabbitRequest>(dataStr);
             
            return data;
        }
        public ConsumeArgs(byte[] data)
        {
            this.Data = data;   
        }
    }
}
