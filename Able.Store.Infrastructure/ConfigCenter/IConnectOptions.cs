using System;

namespace Able.Store.Infrastructure.ConfigCenter
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
