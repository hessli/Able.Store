using System;

namespace Able.Store.IService
{
    public  class ServiceInvalidException : Exception
    {
        public ServiceInvalidException(string message) : base(message)
        {

        }
    }
}
