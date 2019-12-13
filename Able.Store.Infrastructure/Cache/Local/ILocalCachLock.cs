using System.Threading;

namespace Able.Store.Infrastructure.Cache.Local
{
    internal  interface ILocalCachLock
    {
        ReaderWriterLockSlim GetLock();
    }
}
