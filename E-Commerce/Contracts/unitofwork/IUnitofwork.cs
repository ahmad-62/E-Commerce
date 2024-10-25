namespace E_Commerce.Contracts.unitofwork
{
    public interface IUnitofwork:IDisposable
    {
        ICategoryService categories { get; }
        IProductService products { get; }
        IAccountService AccountManager { get; }
        IWishListService wishListService { get; }
        IWishListItemService wishListItemService { get; }
        ICartService cartService { get; }
        ICartItemService cartItemService { get; }
        IOrderService orderService { get; }
        Task<int> complete();
    }
}
