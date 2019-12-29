using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.UniOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.BackService.Repository
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void RegisterAmended<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void RegisterNew<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void RegisterRemoved<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
