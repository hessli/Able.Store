using Able.Store.InfrsturctureProvider.Domain;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Able.Store.InfrsturctureProvider.Repository
{
    public class EFUnitOfWork : StoreProviderContext, IEFUnitOfWork
    {
        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public void RegisterAmended<T>(T entity ) where T:class
        {
            Entry<T>(entity).State = EntityState.Modified;
        }

        public void RegisterNew<T>(T entity) where T : class
        {
            Entry<T>(entity).State = EntityState.Added;
        }

        public virtual void RegisterRemoved<T>(T entity) where T : class
        {
            Entry<T>(entity).State = EntityState.Deleted;
        }

        DbSet<T> IEFUnitOfWork.Souce<T>()
        {
            return  Set<T>();
        }
    }
}
