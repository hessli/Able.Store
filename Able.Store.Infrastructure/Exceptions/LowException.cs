using System;

namespace Able.Store.Infrastructure.Exceptions
{
    public class LowException : AbstractException
    {
        public LowException(string message) : base(message)
        {
        }

        public LowException(string message, Exception ex) : base(message, ex)
        {
        }

        public override int LEVEL => ConstExceptionLevel.LOW;
    }
}
