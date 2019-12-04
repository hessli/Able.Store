using RabbitMQ.Client;
using System;
using System.Threading;
namespace Able.Store.Infrastructure.Queue.Rabbit.RabbitTempContainer
{
    public class RabbitConnectionFactory: IDisposable
    {
        public string TagName
        {
            get
            {
                return Options.TagName;
            }
        }
        private object _synch = new object();
        public RabbitConnectOptions Options { get; private set; }

        private IConnection _connection;
        public IConnection Connection
        {
            get
            {
                if (_connection == null || !_connection.IsOpen)
                {
                   CreateConnection();
                }
                return _connection;
            }
        }
        private ConnectionFactory _connectionFactory;
        public RabbitConnectionFactory(RabbitConnectOptions options)
        {
            Options = options;
            _connectionFactory = new ConnectionFactory
            {
                HostName = Options.Host,
                Password = Options.PassWord,
                UserName = Options.Account,
                Port = Options.Port,
                AutomaticRecoveryEnabled=options.AutomaticRecoveryEnabled
            };
        }
        private void CreateConnection()
        {
            lock (_synch)
            {
                _connection = _connectionFactory.CreateConnection();
                if (!_connection.IsOpen && Options.AutomaticRecoveryEnabled)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (_connection.IsOpen)
                            break;

                        Thread.Sleep(10);
                    }
                }
            }
        }
        public IModel CreatChannel()
        {
            var channel = Connection.CreateModel();

            return channel;
        }
        public void Dispose()
        {
            lock (_synch)
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
                _connectionFactory = null;
            }
            _synch = null;
        }
    }
}
