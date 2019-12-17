using System;

namespace Able.Store.Infrastructure.Exceptions
{
    public abstract class AbstractException : Exception
    {
        public AbstractException(string message):this(message,null)
        {

        }

        public AbstractException(string message, Exception ex) : base(message)
        {
            Time = DateTime.Now;
            this.ExceptionSource = ex;
        }
        public abstract int LEVEL { get; }

        public DateTime Time { get; private set; }

        public Exception ExceptionSource { get; private set; }
        

    }
}
