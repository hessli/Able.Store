using System.Collections.Generic;

namespace Able.Store.QueueService.Interface.Orders
{
    public  class LogisticsRequestBody
    {


        private IList<LogisticsCommdityBody> _commdity;
        public LogisticsRequestBody()
        {

            _commdity = new List<LogisticsCommdityBody>();

            this.Commdity = _commdity;
           
        }
        public string OrderCode { get; set; }
        public decimal Cost { get; set; }
        public decimal OtherCost { get; set; }

        public double Weight { get; set; } = 1.00;

        public double Volume { get; set; } = 1.00;
        public int Quantity { get; set; }
        public LogisticsContactBody Sender { get; set; }
        public LogisticsContactBody Receiver { get; set; }
        public IEnumerable<LogisticsCommdityBody> Commdity { get; set; }
        public string Remark { get; set; }
        public int OrderId { get; set; }

        public void AddItem(LogisticsCommdityBody item)
        {
            this._commdity.Add(item);
        }
    }
}
