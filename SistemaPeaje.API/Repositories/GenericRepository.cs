using Microsoft.EntityFrameworkCore;
using SistemaPeaje.API.Data;
using System.Linq.Expressions;

namespace SistemaPeaje.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TollSystemContext _context;
        protected readonly DbSet<T> _dbSet;
        
        public GenericRepository(TollSystemContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }
        
        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }
        
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        
        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }
        
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? expression = null)
        {
            if (expression == null)
                return await _dbSet.CountAsync();
            
            return await _dbSet.CountAsync(expression);
        }
        
        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }
    }
}
