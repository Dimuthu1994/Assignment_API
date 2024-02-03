using Assignment_API.Data;
using Assignment_API.Models;
using Assignment_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Assignment_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> filter = null)
        {
            IQueryable<Product> query = _db.Products;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Product> query = _db.Products;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Product entity)
        {
            _db.Products.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _db.Products.Update(entity);
            await SaveAsync();
        }
    }
}
