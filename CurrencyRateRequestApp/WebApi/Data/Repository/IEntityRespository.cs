using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApi.Core.Entities.Abstract;

namespace WebApi.Data.Repository
{
    public interface IEntityRespository<T> where T:class,IEntity,new()
    {
        T Get(Expression<Func<T,bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
