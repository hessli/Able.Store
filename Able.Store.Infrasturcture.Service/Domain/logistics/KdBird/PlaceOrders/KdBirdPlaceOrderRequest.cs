﻿using Able.Store.Infrasturcture.Service.Interface.logistics;
using System.Collections.Generic;


namespace Able.Store.Infrasturcture.Service.Domain.Logistics.KdBird
{
    public class KdBirdPlaceOrderRequest : IPlaceOrderRequest
    {
        public KdBirdPlaceOrderRequest()
        {
            this.Commdity = new List<ICommdity>();
        }
        public string OrderCode { get; set; }
        public string ShipperCode { get; set; }
        public int ExpType { get; set; }
        public int PayType { get; set; }
        public decimal Cost { get; set; }
        public decimal OtherCost { get; set; }
        public IContact Sender { get; set; }
        public IContact Receiver { get; set; }
        public IList<ICommdity> Commdity { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        public double Volume { get; set; }
        public string Remark { get; set; }
        public bool IsReturnPrintTemplate { get; set; }
    }
}
