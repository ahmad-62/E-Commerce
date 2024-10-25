using E_Commerce.Dtos.Product;
using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface IProductService
    {
        Task<ProductDisplayDto> GetById(int id);
        Task<IEnumerable<ProductDisplayDto>> GetAll();
        Task<Product> Add(ProductDto Product);
        Task<Product> Update(ProductDto Product, int id);
        Task<Product> GetProduct(int id);
        Task<bool> Delete(int id);
    }
}
