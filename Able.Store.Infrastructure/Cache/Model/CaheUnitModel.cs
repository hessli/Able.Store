using StackExchange.Redis;
using System;
namespace Able.Store.Infrastructure.Cache.Model
{
    public class CacheUnitModel : AbstractCacheModel
    {
        readonly TimeSpan LOWEST_EXPIRE = TimeSpan.FromMinutes(2);

        private bool IsSetExpire()
        {
            return _timeSpane.HasValue && _timeSpane.Value >= LOWEST_EXPIRE;
        }
        private TimeSpan? _timeSpane;
        protected virtual int GetRandomNum()
        {
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));

            var data = ra.Next();

            return data;
        }
        public TimeSpan? Expire
        {
            get
            {
                if (IsSetExpire())
                {
                    var randTime = GetRandomNum();
                    var time = this._timeSpane.Value.Add(TimeSpan.FromSeconds(randTime));
                    return time;
                }
                return default(TimeSpan?);
            }
            set
            {
                _timeSpane = value;
            }
        }
        internal When GetWhen()
        {
            switch (this.CacheStrategy)
            {
                case CacheStrategy.NoExist:
                    return When.NotExists;
                case CacheStrategy.Exist:
                    return When.Exists;
                default: return When.Always;

            }

        }

        public CacheStrategy CacheStrategy
        {
            get; set;
        } = CacheStrategy.Always;
    }
}
