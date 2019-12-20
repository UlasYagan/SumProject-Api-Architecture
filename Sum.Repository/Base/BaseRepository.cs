using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sum.Domain.Entity;
using Sum.Model.Options;

namespace Sum.Repository.Base
{
    public class BaseRepository<T, IdType> : IBaseCrudRepository<T, IdType> where T : class
    {
        protected readonly NorthwindContext _dbContext;
        public BaseRepository(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ICollection<T> Get(Expression<Func<T, bool>> filter = null, DataPagingOptions pagingOptions = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();

            if (filter != null) queryable = queryable.Where(filter);

            if (pagingOptions?.PageSize > 0 && pagingOptions.PageNumber.GetValueOrDefault() > 0)
                queryable = queryable
                    .Skip(pagingOptions.PageNumber.GetValueOrDefault() * pagingOptions.PageSize.GetValueOrDefault())
                    .Take(pagingOptions.PageSize.GetValueOrDefault());

            return queryable.ToList();
        }

        public T GetById(IdType id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public T Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public bool Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteById(IdType id)
        {
            _dbContext.Set<T>().Remove(GetById(id));
            _dbContext.SaveChanges();
            return true;
        }

        public int GetTotalRecordCount()
        {
            return _dbContext.Set<T>().Count();
        }
    }
}