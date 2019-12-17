using System;

namespace Able.Store.Infrastructure.Exceptions
{
    public class HeighException : AbstractException
    {
        public HeighException(string message) : base(message)
        {
        }

        public HeighException(string message, Exception ex) : base(message, ex)
        {

        }
        public override int LEVEL => ConstExceptionLevel.HIGH;
    }

}


