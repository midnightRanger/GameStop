using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;

public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;
    private readonly ApplicationContext _db;
    private readonly ICart _cartRepository;
    private readonly IUser _userRepository;
    private readonly IEkey _ekeyRepository;
    private UserModel _user;
    private List<CartModel> _cartList; 
    private List<UserModel> _userList;
    
    public CartController(ILogger<CartController> logger, 
        ApplicationContext db, 
        ICart cartRepository, IUser userRepository, IEkey ekeyRepository
    )
    { 
        _logger = logger;
        _db = db;
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _ekeyRepository = ekeyRepository;
        _cartList = cartRepository.getAll().Include(c => c.Ekeys).
            ThenInclude(e=>e.Product).ThenInclude(p=>p.ProductInfo).ToList(); 
        _userList = userRepository.getAll().Include(u => u.Account).ToList();
      

    }

    [HttpGet]
    public async Task<IActionResult> Cart()
    {
        _user = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name);
        CartModel _cart = _cartList.FirstOrDefault(c => c.OwnerId == _user.Id);
        return View(_cart); 
    }

    public async Task<IActionResult> AddToCart(int? id)
    {
        _user = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name);
        EKeyModel ekey = _ekeyRepository.getAll().FirstOrDefault(k => k.ProductId == id && k.CartId == null); 
        CartModel _cart = _cartList.FirstOrDefault(c => c.OwnerId == _user.Id);

        if (ekey == null)
        {
            return NotFound(); 
        }
        ekey.CartId = _cart.Id;
        
        _ekeyRepository.updateEkey(ekey);
        _db.SaveChangesAsync();

        return RedirectToAction("Cart", "Cart");
    }
}