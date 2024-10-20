using Algena.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Core.DataAccess.EntityFrameworkCore
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity, new()
    {
        DbContext _db;
        DbSet<T> _dbSet;

        public RepositoryBase(DbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task AddAsync(T entity) => _dbSet.Add(entity);
        public async Task DeleteAsync(T entity) => _dbSet.Remove(entity);
        public async Task UpdateAsync(T entity) => _dbSet.Update(entity);

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
            => expression is not null ? _dbSet.Where(expression).ToList() : _dbSet.ToList();

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression) => await _dbSet.FirstOrDefaultAsync(expression);

        //await varsa her 2 tarafta da async olması lazım.
        //public async Task<int> SaveAsync() => await _db.SaveChangesAsync();

    }
}
