using System;
using System.Collections;

namespace Able.Store.Infrastructure.Serve
{
   public interface IConnectOptions: IComparable
    {
        string TagName { get; }
        string Host { get; }
        int Port { get; }
        string PassWord { get; }
        string Account { get; }
    }
}
