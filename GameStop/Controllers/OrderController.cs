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
    
    private readonly ICart _cartRepository;
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService; 
    private readonly IUser _userRepository;
    private readonly List<UserModel> _userList;
    private readonly List<CartModel> _cartList;
    private readonly IOrder _orderRepository;
    public OrderController(ILogger<OrderController> logger, 
        ApplicationContext db,
        IUser userRepository,
        ICart cartRepository, IOrderService orderService, IOrder orderRepository, ICartService cartService)
    {
        _cartRepository = cartRepository;
        _orderService = orderService;
        _orderRepository = orderRepository;
        _cartService = cartService;
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
        return View(orderView);
    }

    public async Task<IActionResult> OrderConfirm()
    {
        var user = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name);
        var cart = _cartList.FirstOrDefault(c=> c.Id == user.Cart[0].Id);
        
        var response = await _orderService.MakeOrder(user, cart);
        var responseCart = await _cartService.ClearCart(user);

        if (response.StatusCode == GameStop.StatusCode.OK && responseCart.StatusCode == GameStop.StatusCode.OK)
        {
            return RedirectToAction("Orders", "Order");
        }
        return RedirectToAction("MakeOrder", "Order", new {error = response.Description});
    }
    
    public async Task<IActionResult> Orders()
    {
        List<OrderViewModel> productView = new List<OrderViewModel>();
        
        var user = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name);
        var orders = _orderRepository.getAll()
            .Include(o => o.EKeys)
            .ThenInclude(e => e.Product)
            .ThenInclude(p => p.ProductInfo).Where(o=>o.UserId == user.Id);

        foreach (var order in orders)
        {
            productView.Add(new OrderViewModel()
            {
                DateTime = order.DateTime,
                Ekeys = order.EKeys, Sum = order.Sum, User = user
            });
        }
        return View(productView); 
    }


}