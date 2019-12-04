using Able.Store.Infrastructure.Cache.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Able.Store.Infrastructure.Queue.Rabbit.Notify
{
    public class RabbitResponse
    {
        public string CorrelationId  {
            get;
            private set;
        }
        private int _waitTime=5000;
        private bool _isSynch=false;
        private bool _isGetNotify = false;
        private int _moduleId=0;
        internal RabbitResponse(
            string _requestCorrelationId,
            bool isGetNotify,
            int waitTime,bool isSynch,int moduleId=0)
        {
            CorrelationId = RabbitResponseResult
                .GetCorrelationId(_requestCorrelationId);

            this._waitTime = waitTime;

            this._isSynch = isSynch;

            _moduleId = moduleId;

            _isGetNotify = isGetNotify;
        }
        private CacheController _cacheController;

        internal RabbitResponseResult CreateResult()
        {
           var notifyArgs = new RabbitResponseResult
            {
                IsSuccess = true
            };

            return notifyArgs;
        }
        internal RabbitResponseResult GetResult(Action<RabbitResponseResult> synchCallback, 
            CacheController cacheController=null)
        {
            RabbitResponseResult notifyArgs=null;

            if (_isGetNotify)
            {
                    _cacheController = cacheController;

                    if (_isSynch && synchCallback!=null)
                    {
                        Task task = new Task(() =>
                        {
                             notifyArgs = RequestResult();
                            synchCallback?.Invoke(notifyArgs);
                        });
                        task.Start();
                    }
                    else
                    {
                        notifyArgs = RequestResult();
                    }
            }
            else
            {
                notifyArgs= CreateResult();
                 cacheController = null;
            }
            return notifyArgs;
        }
        private RabbitResponseResult RequestResult()
        {
            var result = default(RabbitResponseResult);

            for (var i = 0; i < 10; i++)
            {
                result = _cacheController.GetEntity<string, RabbitResponseResult>(CorrelationId, _moduleId);
                Thread.Sleep(5);
            }
            if (result != null)
                return result;
            if (this._waitTime - 50 > 0)
            {
                Thread.Sleep(this._waitTime - 50);
            }
            result = _cacheController.GetEntity<string, RabbitResponseResult>(CorrelationId, _moduleId);

            _cacheController = null;

            return result;
        }

        internal static RabbitResponse CreateResponse(string requestCorrelationId, bool isGetNotify, int waitTime, bool isSynch,int moduleId=0)
        {
            RabbitResponse response = new RabbitResponse(requestCorrelationId,isGetNotify, waitTime,isSynch, moduleId);

            return response;
        }
    }
}
