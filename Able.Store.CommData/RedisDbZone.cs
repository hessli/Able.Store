namespace Able.Store.CommData
{
    public enum RedisDbZone : int
    {
        /// <summary>
        /// 通用
        /// </summary>
        Comm=0,
        /// <summary>
        /// 产品
        /// </summary>
        Pms = 1,
        /// <summary>
        /// 用户
        /// </summary>
        Ums = 2,
        /// <summary>
        /// 订单系统
        /// </summary>
        Oms = 3
    }
}
