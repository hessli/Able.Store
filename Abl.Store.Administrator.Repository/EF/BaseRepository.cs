using Able.Store.Adminstrator.Model.EF;
using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.Reflect;
using Able.Store.Infrastructure.UniOfWork;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Abl.Store.Administrator.Repository.EF
{
    public abstract class BaseRepository<T> : IRepository<T>
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

        /// <summary>
        /// 显示的调用导航属性
        /// </summary>
        /// <returns></returns>
        public T GetExplicitFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            var baseQuery = this.Entities;

            ReflectEntity reflectEntity = new ReflectEntity(typeof(T));

            if (reflectEntity.IncludePropertyNames.Count > 0)
            {
                foreach (var item in reflectEntity.IncludePropertyNames)
                {
                    baseQuery = baseQuery.Include(item);
                }
            }
            var entity = baseQuery.FirstOrDefault(expression);

            return entity;
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
    }
}
