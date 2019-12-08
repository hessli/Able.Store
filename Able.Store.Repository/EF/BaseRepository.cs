using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Able.Store.Infrastructure.Querying;

namespace Able.Store.Repository.EF
{
    public abstract class BaseRepository<T> : IEFReadOnlyRepository<T>, IRepository<T>
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
        public IQueryable<T> GetList(Expression<Func<T, bool>> expression, params string[] includes)
        {

            var queryable = this.Entities.Where(expression);

            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    queryable = queryable.Include(item);
                }
            }
            return queryable;
        }

        public IEnumerable<T> GetList(Query query)
        {
           var expression= query.CreateExpression<T>();

           var queryable = this.Entities.Where(expression).Order(query.OrderByProperty);

            return queryable;
        }
        public T GetFirstOrDefault(Query query)
        {
            var expression = query.CreateExpression<T>();

            var data = this.Entities.Where(expression).FirstOrDefault();

            return data;
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
