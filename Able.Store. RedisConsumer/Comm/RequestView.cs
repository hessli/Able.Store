namespace Able.Store.RedisConsumer.Comm
{
    public  class RequestView
    {
        public int errcode { get; set; }

        public string message { get;  set; }

        public bool issuccess { get;  set; }
    }

    public class RequestView<T>:RequestView
    {

        public T result { get; set; }
    }
}
