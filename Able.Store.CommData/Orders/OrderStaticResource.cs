using System;

namespace Able.Store.CommData.Orders
{
    public class OrderStaticResource
    {
        public static readonly string PREFIX = "order.";

        public static readonly string GENERATENO = string.Concat(PREFIX, "sale");

        public static readonly string GENERATENO_FILED = string.Concat(GENERATENO, ".no");

        public static int DBINDEX = (int)RedisDbZone.Oms;

        public static readonly string CREATE_ORDER_CHANGEQTY_CHANNELNAME = string.Concat(PREFIX, "sale.changeqty.channel");

        public static readonly string CREATE_ORDER_CHANGEQTY_RESULT_STUFF = ".complete";
        public static string GetCreateOrderCompleteCorrelationId(string requestCorrelationId)
        {
            return string.Concat(requestCorrelationId, CREATE_ORDER_CHANGEQTY_RESULT_STUFF);
        }
        public static TimeSpan? GetTimeSpan(OrderActionType actionType)
        {
            TimeSpan? timeSpane = null;
            switch (actionType)
            {
                case OrderActionType.订购:
                    timeSpane = TimeSpan.FromDays(1);
                    break;
            }
            return timeSpane;
        }
    }
}
