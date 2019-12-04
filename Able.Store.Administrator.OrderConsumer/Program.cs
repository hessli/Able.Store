using Able.Store.Administrator.OrderConsumer.Business;
using System;
using System.Threading.Tasks;

namespace Able.Store.Administrator.OrderConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectManager.Register();

            ObjectManager.Start();

            ChangeCannotQtyBusiness consumer = new ChangeCannotQtyBusiness();

            Task task = new Task(() =>
            {
                consumer.ChangeCannotQtyConsumer();
            });
            task.Start();

            Console.ReadLine();
        }
    
    }
}
