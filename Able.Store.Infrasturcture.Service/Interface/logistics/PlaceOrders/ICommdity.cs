using System.Collections.Generic;

namespace Able.Store.Infrasturcture.Service.Interface.logistics
{
    public interface ICommdityItem
    {

    }

   public interface ICommdity
    {

        string GoodsName { get; set; }

        int Goodsquantity { get; set; }

        double GoodsWeight { get; set; }
     
       
    }
}
