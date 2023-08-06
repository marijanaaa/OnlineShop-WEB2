using Microsoft.EntityFrameworkCore;
using online_selling.Infrastructure;
using online_selling.Interfaces.Repository;

namespace online_selling.Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected OnlineShopDbContext _context { get; set; }

        public GenericRepository(OnlineShopDbContext context) { 
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            return result.Entity;
        }
        
        public T AddSync(T entity)
        {
            var result = _context.Set<T>().Add(entity);
            return result.Entity;
        }

        public T UpdateSync(T entity)
        {
            var result = _context.Set<T>().Update(entity);
            return result.Entity;
        }

        public async Task<List<T>>? GetAllAsync()
        {
            var items = await _context.Set<T>().ToListAsync();
            return items;
        }

        public void UpdateEntitiesSync(List<T> entities)
        {
            foreach (var entity in entities)
            {
               _context.Set<T>().Update(entity);
            }
        }
    }
}
