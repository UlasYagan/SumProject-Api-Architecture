using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sum.Model.Options;

namespace Sum.Repository.Base
{
    public interface IBaseCrudRepository<T, IdType>
    {
        ICollection<T> Get(Expression<Func<T, bool>> filter = null, DataPagingOptions pagingOptions = null);

        T GetById(IdType id);

        T Create(T entity);

        T Update(T entity);

        bool Delete(T entity);

        bool DeleteById(IdType id);

        int GetTotalRecordCount();
    }
}