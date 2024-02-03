using Assignment_API.Models;
using System.Linq.Expressions;

namespace Assignment_API.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> filter = null);

        Task<Product> GetAsync(Expression<Func<Product, bool>> filter = null, bool tracked = true);

        Task CreateAsync(Product entity);

        Task UpdateAsync(Product entity);

        Task RemoveAsync(Product entity);

        Task SaveAsync();
    }
}
