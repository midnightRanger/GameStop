using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;

public class OrderController: Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly ApplicationContext _db;
    private readonly IOrder _orderRepository;
    private readonly IEkey _ekeyRepository;
    private readonly ICart _cartRepository;
    private readonly IUser _userRepository;
    private readonly List<UserModel> _userList;
    private readonly List<CartModel> _cartList;
    private OrderViewModel _orderView;

    public OrderController(ILogger<OrderController> logger, 
        ApplicationContext db, 
        IOrder orderRepository,
        IEkey ekeyRepository,
        IUser userRepository,
        ICart cartRepository
    )
    {
        _orderRepository = orderRepository;
        _ekeyRepository = ekeyRepository;
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _logger = logger;
        _db = db;
        _cartList = cartRepository.getAll().Include(c => c.Ekeys).ThenInclude(e => e.Product)
            .ThenInclude(p => p.ProductInfo).ToList();
        _userList = userRepository.getAll().Include(u => u.Account).Include(c => c.Cart).
            ThenInclude(e => e.Ekeys).ThenInclude(p=>p.Product).ToList();
    }

    public async Task<IActionResult> MakeOrder(OrderViewModel orderView)
    {
        var user = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name);
        var cart = await _cartRepository.getCart(user.Cart[0].Id);
        double sum = 0;
        foreach (var el in cart.Ekeys)
        {
            sum = el.Product.Cost + sum;
        }

        orderView = new()
        {
            Cart = cart,
            DateTime = DateTime.Now,
            User = user,
            Ekeys = cart.Ekeys,
            Sum = sum
        };
        _orderView = orderView;
        return View(orderView);
    }

    public async Task<IActionResult> OrderConfirm(OrderViewModel orderView)
    {
        var user = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name);
        var cart = _cartList.FirstOrDefault(c=> c.Id == user.Cart[0].Id);
        double sum = 0;
        foreach (var el in cart.Ekeys)
        {
            sum = el.Product.Cost + sum;
        }

        orderView = new()
        {
            Cart = cart,
            DateTime = DateTime.Now,
            User = user,
            Ekeys = cart.Ekeys,
            Sum = sum
        };
        
        if (user.Balance < orderView.Sum)
        {
            return NotFound();
        }
        
        user.Balance -= orderView.Sum;

        OrderModel order = new()
        {
            Sum = orderView.Sum,
            DateTime = DateTimeOffset.Now,
            EKeys = user.Cart[0].Ekeys,
            TransactionDataModel = null,
            User = user,
            TransactionDataId = null
        };
        await _orderRepository.addOrder(order);
        int id = _orderRepository.getAll().OrderBy(o=>o.Id).LastOrDefault().Id;

        foreach (var keys in user.Cart[0].Ekeys.ToList())
        {
            keys.CartId = null;
            keys.OrderId = id; //TODO get 
             _ekeyRepository.updateEkey(keys);
        }
        //  await _db.SaveChangesAsync();
        
       return RedirectToAction("Orders", "Order");
    }
    
    public async Task<IActionResult> Orders()
    {
        return View(); 
    }


}