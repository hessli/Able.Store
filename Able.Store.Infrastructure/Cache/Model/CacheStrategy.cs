namespace Able.Store.Infrastructure.Cache.Model
{
    public  enum CacheStrategy
    {
        /// <summary>
        /// 不存在则新增，存在则覆盖
        /// </summary>
         Always,
         /// <summary>
         /// 不存在则新增，存在则忽略
         /// </summary>
         NoExist,
         /// <summary>
         /// 存在做覆盖，不存在则忽略
         /// </summary>
         Exist
    }

}
