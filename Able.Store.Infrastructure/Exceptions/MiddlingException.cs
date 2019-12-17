using System;

namespace Able.Store.Infrastructure.Exceptions
{
    public class MiddlingException : AbstractException
    {
        public MiddlingException(string message) : base(message)
        {
        }

        public MiddlingException(string message, Exception ex) : base(message, ex)
        {
        }

        public override int LEVEL => ConstExceptionLevel.MIDDLING;
    }
}
