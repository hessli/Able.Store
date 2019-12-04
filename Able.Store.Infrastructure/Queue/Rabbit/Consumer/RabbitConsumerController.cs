namespace Able.Store.Infrastructure.Queue.Rabbit.Consumer
{
    public  class RabbitConsumerController:RabbitBaseController
    {
        public event ConsumeHandle OnConsumeHandler;
        private RabbitConsumer _consumer;
        public void RabbitConsume(RabbitConsumerOption option)
        {
            if (OnConsumeHandler != null)
            {
                _consumer = new RabbitConsumer();

                _consumer.Factory = base.ConnectionFactory;

                _consumer.OnConsumeHandler += OnConsumeHandler;

                _consumer.Consume(option);
            }
        }
    }
}
