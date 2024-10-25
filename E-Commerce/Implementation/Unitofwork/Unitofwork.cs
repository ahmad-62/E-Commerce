using E_Commerce.Contracts;
using E_Commerce.Contracts.unitofwork;
using E_Commerce.Models.Data;

namespace E_Commerce.Implementation.Unitofwork
{
    public class Unitofwork : IUnitofwork
    {

        public ICategoryService categories {  get; private set; }
       public  IProductService products { get; private set; }

        public IAccountService AccountManager { get; private set; }

        public IWishListService wishListService { get; private set; }
        public IWishListItemService wishListItemService { get; private set; }

        public ICartService cartService { get; private set; }

        public ICartItemService cartItemService { get; private set; }

        public IOrderService orderService {  get; private set; }

        private readonly AppDbContext context;
        public Unitofwork(AppDbContext context,ICategoryService categoryService,IProductService productService,IAccountService accountService,IWishListService wishListService,IWishListItemService wishListItemService, ICartService cartService, ICartItemService cartItemService,IOrderService orderService)

        {
            this.wishListService = wishListService;
            AccountManager= accountService;
            categories = categoryService;
            products = productService;
            this.context = context; 
            this.wishListItemService= wishListItemService;
            this.cartItemService= cartItemService;
            this.cartService= cartService;
            this.orderService= orderService;
        }
        public async Task<int> complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
