using System;

namespace Able.Store.Administrator.IService
{
    public class ServiceInvalidException : Exception
    {
        public ServiceInvalidException(string message) : base(message)
        {

        }
    }
}
