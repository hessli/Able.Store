namespace Able.Store.Administrator.IService
{
    public class ResponseView
    {

        public ResponseView(string message, bool isSuccess)
        {
            this.message = message;

            this.issuccess = isSuccess;
        }
        public int errcode { get; set; }

        public string message { get; private set; }

        public bool issuccess { get; private set; }
    }
    public class ResponseView<T> : ResponseView
    {
        public ResponseView(string message, bool isSuccess, T result) : base(message, isSuccess)
        {
            this.result = result;
        }
        public T result { get; private set; }
    }
}
