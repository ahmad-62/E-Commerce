using AutoMapper;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Dtos.Product;
using E_Commerce.Models;

namespace E_Commerce.Implementation
{
    public class ProductService:IProductService
    {

        private readonly IBaseRepository<Product> Product;
        private readonly IMapper mapper;
        public ProductService(IBaseRepository<Product> Product, IMapper mapper)
        {
            this.Product = Product;
            this.mapper = mapper;
        }
        public async Task<Product> Add(ProductDto _Product)
        {
            try
            {
                var NewProduct = mapper.Map<Product>(_Product);
                await Product.Add(NewProduct);
                return NewProduct;



            }
            catch (Exception ex)
            {
                throw new Exception("There is Exception when Adding the Product", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var _Product = await Product.GetById(id);
                if (_Product is null)
                    return false;
                Product.Delete(_Product);
                return true;


            }
            catch (Exception ex)
            {
                throw new Exception("There is Exception in Deleting The Product", ex);
            }
        }

        public async Task<IEnumerable<ProductDisplayDto>> GetAll()
        {
            try
            {
                var products = await Product.Get(null, x=>x.Category);
                return mapper.Map<IEnumerable<ProductDisplayDto>>(products); 
            }
            catch (Exception ex)
            {
                throw new Exception("There is Exception when Getting Products", ex);
            }
        }

        public async Task<ProductDisplayDto> GetById(int id)
        {
            try
            {
                var _Product = await Product.FindAsync(p => p.Id == id,x=>x.Category);
                if (_Product is null)
                    return null; // Handle null cases properly

                return mapper.Map<ProductDisplayDto>(_Product);
            }
            catch (Exception ex)
            {
                // Re-throwing the exception while preserving the original stack trace
                throw new Exception("Error occurred while retrieving the Product.", ex);
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            try {
                return await Product.GetById(id);
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> Update(ProductDto UpdatedProduct, int id)
        {
            try
            {
                var _Product = await Product.GetById(id);
                if (_Product is null)
                    return null;
                if(UpdatedProduct.CategoryId==0)
                    UpdatedProduct.CategoryId = _Product.CategoryId;    
                _Product = mapper.Map(UpdatedProduct, _Product);
                return _Product;

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the Product.", ex);
            }

        }
    }
}

