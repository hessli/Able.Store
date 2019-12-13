using StackExchange.Redis;
using System;
using System.Threading;

namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisConnectionFactory : IDisposable
    {

        public string TagName { get; set; }
        private object _synch = new object();
        public RedisConnectOptions Options { get; private set; }
        public bool IsOpend
        {
            get
            {

                return Connection.IsConnected;
            }
        }
        public ConnectionMultiplexer _connection;
        public ConnectionMultiplexer Connection
        {

            get
            {
                if (_connection == null || !_connection.IsConnected)
                {
                    CreateConnection();
                }
                return _connection;
            }
        }
        public RedisConnectionFactory(RedisConnectOptions options)
        {
            this.Options = options;
            this.TagName = Options.TagName;
        }
        private void CreateConnection()
        {
            ConfigurationOptions config = new ConfigurationOptions
            {
                AbortOnConnectFail = Options.AbortOnConnectFail,
                Password = Options.PassWord,
                SyncTimeout = Options.SyncTimeout,
                AllowAdmin = Options.AllowAdmin,
                ConnectRetry = Options.ConnectRetry,
                EndPoints = {
                     Options.ConnctionString
                }
            };
            lock (_synch)
            {
                _connection = ConnectionMultiplexer.Connect(config);
                if (!_connection.IsConnected)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        if (_connection.IsConnected)
                            break;
                        Thread.Sleep(10);
                    }
                }
                config = null;
            }
        }
        public IDatabase GetDatabase(int index)
        {
            var dataBase = this._connection.GetDatabase(index);

            return dataBase;
        }
        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();

            Options = null;
            _connection = null;
            this.TagName = null;
        }
    }
}
