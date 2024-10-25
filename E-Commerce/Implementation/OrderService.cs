using AutoMapper;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Contracts.Payment;
using E_Commerce.Dtos.Order;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Orders;

namespace E_Commerce.Implementation
{
    public class OrderService:IOrderService
    {
        private readonly IBaseRepository<Models.Order> repository;
        private readonly IPaymentService paymentService;
        private readonly IAccountService accountService;
        private readonly ICartItemService cartItemService;
        private readonly IProductService productService;
        
        private readonly IMapper mapper;
        public OrderService(IBaseRepository<Models.Order> repository,IPaymentService paymentService,IAccountService accountService,IMapper mapper,IProductService productService,ICartItemService cartItemService) {
        
        this.repository = repository;
            this.paymentService = paymentService;
            this.accountService = accountService;
            this.mapper = mapper;
            this.productService = productService;
            this.cartItemService = cartItemService;
        }

        public async Task<OrderDto> Addorder()
        {
            try {
                var client = await accountService.GetClientByAppUser();
                if (client is null)
                    throw new Exception();
                var items =  client.shoppingCart.Items;
                if (items is null)
                    throw new Exception("There is no items");
                Models.Order order = new Models.Order
                {
                    Items = items.Select(cartItem => (OrderItem)cartItem).ToList(),
                    client = client,

                   
            };
                var result =await paymentService.ProcessPaymentAsync(order, order.TotalAmount);
                if (result)
                {
                    order.Status = Enums.Status.Completed;
                    foreach(var item in items)
                    {
                        item.Product.InStock-=item.Quantity;
                        if (item.Product.InStock < 0)
                            throw new Exception();
                        



                    }
                    await cartItemService.clearcart(client.shoppingCart.Id);

                }
                else
                    order.Status = Enums.Status.Cancelled;
                await repository.Add(order);
                return mapper.Map<OrderDto>(order);
                    
                

            
            
            
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try {
                var order = await repository.GetById(id);
                if (order is null)
                    throw new Exception();
                repository.Delete(order);
                return true;
            
            
            }
            catch(Exception ex) { 
            throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDisplayDto>> GetAll()
        {
            try {

                var client = await accountService.GetClientByAppUser();
                if (client is null)
                    throw new Exception();
                var orders = await repository.GetAllAysnc();
                if (orders is null)
                    throw new Exception();
                return mapper.Map<IEnumerable<OrderDisplayDto>>(orders);
               

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllForUsers()
        {
            try {
                var client = await accountService.GetClientByAppUser();
                if (client is null)
                    throw new Exception();

                if(client.orders is  null)
                    throw new Exception();
                return mapper.Map<IEnumerable<OrderDto>>(client.orders);




            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderDto> GeTById(int id)
        {
            try {
                
                var order=await repository.Find(x=>x.Id==id,x=>x.Include(y=>y.Items).ThenInclude(y=>y.Product));
                if(order is null)
                    throw new Exception();
                return mapper.Map<OrderDto>(order);



            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
