using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Able.Store.Repository.EF
{
    public abstract class BaseRepository<T> : IReadOnlyRepository<T>, IRepository<T>
                                                   where T : class, IAggregateRoot
    {
        private IUnitOfWork _unitOfWork=null;
        protected IEFUnitOfWork EFUnitOfWork
        {
            get
            {
                return (IEFUnitOfWork)_unitOfWork;
            }
        }
        public BaseRepository(IUnitOfWork unitOfWork)
        {

            this._unitOfWork = unitOfWork;
        }
       

        public T GetSingle()
        {
            return EFUnitOfWork.Souce<T>().FirstOrDefault();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return EFUnitOfWork.Souce<T>()
                          .FirstOrDefault(expression);
        }

        public IQueryable<T> Entities
        {
            get
            {
                return this.EFUnitOfWork.Souce<T>();
            }
        }


        public IQueryable<T> GetList(Expression<Func<T, bool>> expression,params string[] includes)
        {
            var query= this.Entities.Where(expression);
            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
        
        public IQueryable<T> GetList(Expression<Func<T, bool>> expression)
        {
            return this.Entities.Where(expression);
        }
        public IQueryable<T> GetList<S>(Expression<Func<T, bool>> expression,
            Expression<Func<T, S>> orderBy,
            bool descending)
        {
            var queryable = this.Entities.Where(expression);

            return descending ? queryable.OrderByDescending(orderBy) :
                                 queryable.OrderBy(orderBy);
        }
        public void Add(T entity)
        {
            _unitOfWork.RegisterNew(entity);
        }
        public virtual void Remove(T entity)
        {
            _unitOfWork.RegisterRemoved(entity);
        }
        public virtual void Save(T entity)
        {
            _unitOfWork.RegisterAmended(entity);
        }
        public virtual void Commit()
        {
            this._unitOfWork.Commit();
        }
    

        public T GetFirstById(int id)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

            var propertyExpression=Expression.Property(parameter,"Id");

            var constant = Expression.Constant(id);

           var conditon=  Expression.Lambda<Expression<Func<T, bool>>>(Expression.Equal(propertyExpression,constant),parameter);

          var data=  this.Entities.FirstOrDefault(conditon.Compile());

            return data;
        }
    } 
}
