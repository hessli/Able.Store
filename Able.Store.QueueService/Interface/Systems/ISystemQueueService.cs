using System;

namespace Able.Store.QueueService.Interface.Systems
{
    public interface ISystemQueueService
    {
        /// <summary>
        /// 系统异常
        /// </summary>
        void PublisSystemException(string actionName, int controllerHashCode,
            string controllerName, Exception exception);
    }
}
