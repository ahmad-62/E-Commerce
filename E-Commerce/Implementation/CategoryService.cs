using AutoMapper;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> category;
        private readonly IMapper mapper;
        public CategoryService(IBaseRepository<Category> category, IMapper mapper)
        {
            this.category = category;
            this.mapper = mapper;
        }
        public async Task<Category> Add(CategoryDto _category)
        {
            try { 
                var NewCategory=mapper.Map<Category>(_category);
                await category.Add(NewCategory);
                return NewCategory;
            
            
            
            }
            catch (Exception ex)
            {
                throw new Exception("There is Exception when Adding the Category",ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try {
                var _category=await category.GetById(id);
                if (_category is null)
                    return false;
                category.Delete(_category);
                return true;
            
            
            }
            catch(Exception ex) {
                throw new Exception("There is Exception in Deleting The Category",ex);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            try
            {
                return mapper.Map<IEnumerable<CategoryDto>>(await category.GetAllAysnc()); ;
            }
            catch (Exception ex)
            {
                throw new Exception("There is Exception when Geting categorries", ex);
            }
        }

        public async Task<CategoryDto> GetById(int id)
        {
            try
            {
                var _category = await category.GetById(id);
                if (_category is null)
                    return null; // Handle null cases properly

                return mapper.Map<CategoryDto>(_category);
            }
            catch (Exception ex)
            {
                // Re-throwing the exception while preserving the original stack trace
                throw new Exception("Error occurred while retrieving the category.", ex);
            }
        }


        public async Task<Category> Update(CategoryDto Updatedcategory, int id)
        {
            try
            {
                var _category = await category.GetById(id);
                if (_category is null)
                    return null;
                _category = mapper.Map(Updatedcategory, _category);
                return _category;

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the category.", ex);
            }

        }
    }
}

