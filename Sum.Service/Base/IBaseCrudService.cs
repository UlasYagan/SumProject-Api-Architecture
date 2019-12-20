using Sum.Model.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sum.Service.Base
{
    public interface IBaseCrudService<T, IdType>
    {
        ICollection<T> Get(Expression<Func<T, bool>> filter = null, DataPagingOptions dataPagingOptions = null);

        T GetById(IdType id);

        T Create(T entity);

        T Update(T entity);

        bool Delete(T entity);

        bool DeleteById(IdType id);

        int GetTotalRecordCount();
    }
}