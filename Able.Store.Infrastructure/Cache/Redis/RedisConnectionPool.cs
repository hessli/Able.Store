﻿using Able.Store.Infrastructure.ConfigCenter;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisConnectionPool
    {
        private RedisConnectionPool()
        {
             
        }
        public static RedisConnectionPool Instance
        {

            get; private set;
        }

        static RedisConnectionPool()
        {
            Instance = new RedisConnectionPool();
        }
        object _synch = new object();

        Dictionary<string, IConnectOptions> _keys = new Dictionary<string, IConnectOptions>();

        ConcurrentDictionary<string, RedisConnectionFactory> _dicFactory = new ConcurrentDictionary<string, RedisConnectionFactory>();

        ConcurrentQueue<RedisConnectionFactory> _queue = new ConcurrentQueue<RedisConnectionFactory>();
        public bool IsEmpty
        {
            get
            {
                return _queue.IsEmpty;
            }
        }
        int _count = 0;
        public void Add(IConnectOptions connectOptions, RedisConnectionFactory factory)
        {
            lock (_synch)
            {
                Interlocked.Increment(ref _count);

                IConnectOptions oldConnectOptions = null;
                if (_keys.TryGetValue(connectOptions.TagName, out oldConnectOptions))
                {
                    RedisConnectionFactory oldFactory = null;
                    //但是属性已发生变更
                    if (connectOptions.CompareTo(oldConnectOptions) == 0)
                    {
                        _keys.Remove(connectOptions.TagName);
                        _queue.TryDequeue(out oldFactory);

                        //重新入队
                        _keys.Add(connectOptions.TagName, connectOptions);
                        _queue.Enqueue(factory);
                    }
                }
                else
                {
                    _keys.Add(connectOptions.TagName, connectOptions);
                    _queue.Enqueue(factory);

                }
                Interlocked.Decrement(ref _count);
            }
        }
        public bool TryGet(out RedisConnectionFactory factory)
        {
            RedisConnectionFactory temp = null;
            lock (_synch)
            {
                if (this.Get(out temp))
                {
                    factory = temp;
                    return true;
                }
                else
                {
                    for (var i = 0; i < 5; i++)
                    {
                        if (this.Get(out temp))
                        {
                            factory = temp;
                            return true;
                        }
                        Thread.Sleep(i);
                    }

                    factory = null;

                    return false;
                }
            }
        }

        private bool Get(out RedisConnectionFactory factory)
        {
            RedisConnectionFactory temp = null;

            if (_queue.TryDequeue(out temp))
            {
                factory = temp;
                _queue.Enqueue(temp);

                return temp.IsOpend;
            }
            factory = null;

            return false;
        }
    }
}
