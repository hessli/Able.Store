using System;

namespace Able.Store.Infrastructure.Jobs
{
    internal class JobOption
    {
        internal IJob Job { get; set; }

        internal double Interval { get; set; }

        internal DateTime NextRun { get; set; }  

    }

}
