using System;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Domain.Events
{
    public static class IEunmerableExtensions
    {

        public static void ForEach<T>(this IEnumerable<T> souce,Action<T> action)
        {
            foreach (T item in souce)
            {
                action(item);
            }
        }
    }
}
