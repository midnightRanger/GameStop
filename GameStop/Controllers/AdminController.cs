using System.Text.Json;
using GameStop.DAL.Interface;
using GameStop.DAL.Repository;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;
    private readonly IProduct _productRepository;
    private readonly IEkey _ekeyRepository;
    private readonly IUser _userRepository;
    private List<UserModel> _userList;
    private IEnumerable<ProductModel> _allProduct;

    public AdminController(ILogger<HomeController> logger, ApplicationContext db, IProduct productRepository, IEnumerable<ProductModel> allProduct, IEkey ekeyRepository, IUser userRepository)
    {
        _logger = logger;
        _db = db;
        _productRepository = productRepository;
        _ekeyRepository = ekeyRepository;
        _userRepository = userRepository;
        _userList = userRepository.getAll().Include(u => u.Account).ToList();
        _allProduct = productRepository.getAll().Include(u => u.Platforms).Include(u=>u.ProductInfo).ToList();
    }

    public IActionResult Diagrams()
    {

        List<String> name = new List<string>();
        List<Double> price = new List<Double>();
        name = _allProduct.Select(u => u.ProductInfo.Name).ToList();
        price = _allProduct.Select(u => u.Cost).ToList();

        var json = JsonSerializer.Serialize(name);
        var priceList = string.Join(",", price);

        ViewBag.productNameList = json;
        ViewBag.productPriceList = priceList; 
     
     return View();
    }
    public IActionResult Admin()
    {
        return View();
    }
    
    public async Task<IActionResult> EKeys(string? notification)
    {
        if(notification != null) 
            ModelState.AddModelError("", notification);
        var ekeys = await _ekeyRepository.getAll().Include(e => e.Product).
            ThenInclude(p=>p.ProductInfo).ToListAsync();
        return View(ekeys); 
    }

    [HttpGet]
    public async Task<IActionResult> EKeyUpdate(int? number, EKeyUpdateViewModel updateViewModel)
    {
        var ekey = await _ekeyRepository.getEkey(number);
         if (ekey == null)
        {
            return NotFound();
        }

         List<ProductViewModel> productSelectList = new List<ProductViewModel>();
         foreach (var products in _productRepository.getAll().Include(p=>p.ProductInfo))
         {
             productSelectList.Add(new ProductViewModel()
             {
                 Id = products.Id,
                 Name = products.ProductInfo.Name
             });
         }

        updateViewModel = new()
        {
            EKey = ekey,
            Admin = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name),
            Product = ekey.Product,
            Number = ekey.Number,
            ProductId = ekey.ProductId,
            Products = new SelectList(productSelectList, nameof(ProductViewModel.Id), nameof(ProductViewModel.Name))
        };
        return View(updateViewModel);
    }
    [HttpPost]
    public async Task<ActionResult> EKeyUpdate(EKeyUpdateViewModel eKeyUpdateViewModel)
    {
        EKeyModel ekey = await _ekeyRepository.getEkey(eKeyUpdateViewModel.Number);
        if (ekey == null)
        {
            return NotFound();
        }
        ekey.ProductId = eKeyUpdateViewModel.ProductId;
        await _ekeyRepository.updateEkey(ekey);
        
        
        return RedirectToAction("EKeys", "Admin" ,new { notification = "E-key was updated"  });
    }

    public async Task<IActionResult> EKeyDelete(int? number)
    {
        _ekeyRepository.deleteEkey(number); 
        return RedirectToAction("EKeys", "Admin" ,new { notification = "E-key was deleted"  });
    }
}