using System;

namespace Able.Store.Infrastructure.Domain
{
    public  class ValueObjectIsInvalidException : Exception
    {
        public ValueObjectIsInvalidException(string message) : base(message)
        {

        }
    }
}
