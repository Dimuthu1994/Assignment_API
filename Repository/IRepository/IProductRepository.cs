using Assignment_API.Models;

namespace Assignment_API.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<Product> UpdateAsync(Product entity);
    }

}
