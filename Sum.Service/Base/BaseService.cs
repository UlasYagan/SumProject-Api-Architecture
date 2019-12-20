using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sum.Model.Options;
using Sum.Repository.Base;

namespace Sum.Service.Base
{
    public class BaseService<T, IdType> : IBaseCrudService<T, IdType>
    {
        protected readonly IBaseCrudRepository<T, IdType> _repository;

        protected BaseService(IBaseCrudRepository<T, IdType> repository)
        {
            _repository = repository;
        }

        public ICollection<T> Get(Expression<Func<T, bool>> filter = null, DataPagingOptions pagingOptions = null)
        {
            return _repository.Get(filter, pagingOptions);
        }

        public T GetById(IdType id)
        {
            return _repository.GetById(id);
        }

        public T Create(T entity)
        {
            return _repository.Create(entity);
        }

        public T Update(T entity)
        {
            return _repository.Update(entity);
        }

        public bool Delete(T entity)
        {
            return _repository.Delete(entity);
        }

        public bool DeleteById(IdType id)
        {
            return _repository.DeleteById(id);
        }

        public int GetTotalRecordCount()
        {
            return _repository.GetTotalRecordCount();
        }
    }
}