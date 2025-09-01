using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace ISproject.Repository.Interface
{
    public interface IRepository<T> where T : BaseModel
    {
        T Insert(T entity);

        IEnumerable<T> InsertMany(IEnumerable<T> entities);
        T Update(T entity);
        T Delete(T entity);

        E? Get<E>(
            Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>,IIncludableQueryable<T,object>>? include = null);

        IEnumerable<E> GetAll<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);


    }
}
 