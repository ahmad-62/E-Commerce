using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetById(int id);
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<Category> Add(CategoryDto category);
        Task<Category> Update(CategoryDto category,int id);
        Task<bool> Delete(int id);    
    }
}
