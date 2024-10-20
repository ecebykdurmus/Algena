using Algena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Core.DataAccess
{
    public interface IRepositoryBase<T> where T : class,IEntity,new()
    {
        Task<T> GetAsync(Expression<Func<T,bool>> expression);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task AddAsync(T entity); //void gibi.
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        //Task<int> SaveAsync();

    }
}
