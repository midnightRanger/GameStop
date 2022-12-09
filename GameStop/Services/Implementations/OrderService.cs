using System.Collections.Specialized;
using System.Data.Entity;
using GameStop.DAL.Interface;
using GameStop.DAL.Repository;
using GameStop.Models;
using GameStop.Response;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Services;

public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly ApplicationContext _db;
    private readonly IOrder _orderRepository;
    private readonly IEkey _ekeyRepository;
    private readonly ICart _cartRepository;
    
    public OrderService(IEkey ekeyRepository, IOrder orderRepository, ILogger<OrderService> logger, ApplicationContext db, ICart cartRepository)
    {
        _ekeyRepository = ekeyRepository;
        _orderRepository = orderRepository;
        _logger = logger;
        _db = db;
        _cartRepository = cartRepository;
    }

    public async Task<BaseResponse<bool>> MakeOrder(UserModel user, CartModel cart)
    {
        try
        {
            double sum = 0;
            foreach (var el in cart.Ekeys)
            {
                sum = el.Product.Cost + sum;
            }

            if (user.Balance < sum)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.NoMoney,
                    Description = "You dont have money enough"
                };
            }
            user.Balance -= sum;

            OrderModel order = new()
            {
                Sum = sum,
                DateTime = DateTime.Now,
                EKeys = user.Cart[0].Ekeys.ToList(),
                TransactionDataModel = null,
                User = user,
                TransactionDataId = null
            };
            //await _orderRepository.addOrder(order);
            await _db.Order.AddAsync(order);
            await _db.SaveChangesAsync();

            cart.Ekeys.Clear();
            _db.Cart.Update(cart);
            await _db.SaveChangesAsync();

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Order was successfully confirmed"
            };
        }   
        
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[ConfirmOrder]: {ex.Message}");
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}