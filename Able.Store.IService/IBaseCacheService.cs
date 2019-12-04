using System.Collections.Generic;

namespace Able.Store.IService
{
    public interface IBaseCacheService
    {
        /// <summary>
        /// 缓存前缀
        /// </summary>
        string PREFIX { get; }

        /// <summary>
        /// 所有前缀
        /// </summary>
        IList<string> CacheKeys { get; }
    }
}
