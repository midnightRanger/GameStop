using System.Text.Json;
using GameStop.DAL.Interface;
using GameStop.DAL.Repository;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;
[Authorize(Roles = "ADMIN")]
public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;
    private readonly IProduct _productRepository;
    private readonly IEkey _ekeyRepository;
    private readonly IUser _userRepository;
    private readonly IPlatform _platformRepository; 
    private List<UserModel> _userList;
    private IEnumerable<ProductModel> _allProduct;
    private IEnumerable<PlatformModel> _allPlatform;

    public AdminController(ILogger<HomeController> logger, ApplicationContext db, IProduct productRepository, IEnumerable<ProductModel> allProduct, IEkey ekeyRepository, IUser userRepository, IPlatform platformRepository)
    {
        _logger = logger;
        _db = db;
        _productRepository = productRepository;
        _ekeyRepository = ekeyRepository;
        _userRepository = userRepository;
        _platformRepository = platformRepository;
        _userList = userRepository.getAll().Include(u => u.Account).ToList();
        _allPlatform = platformRepository.getAll().Include(p => p.Products);
        _allProduct = productRepository.getAll().Include(u => u.Platforms).Include(u=>u.ProductInfo).ToList();
    }

    
    public IActionResult Diagrams()
    {

        List<String> name = _allProduct.Select(u => u.ProductInfo.Name).ToList();
        List<Double> price = _allProduct.Select(u => u.Cost).ToList();

        List<String> platformName = _allPlatform.Select(p => p.Name).ToList();
        List<int> gamesOnPlatform = _allPlatform.Select(p => p.Products.Count()).ToList();

        var jsonPlatformName = JsonSerializer.Serialize(platformName);
        var gamesOnPlatformList = string.Join(",", gamesOnPlatform);

        var json = JsonSerializer.Serialize(name);
        var priceList = string.Join(",", price);

        ViewBag.productNameList = json;
        ViewBag.productPriceList = priceList;

        ViewBag.platformNames = jsonPlatformName;
        ViewBag.platformGames = gamesOnPlatformList; 
        
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
    public async Task<IActionResult> EKeyAdd(EKeyUpdateViewModel updateViewModel)
    {
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
            Products = new SelectList(productSelectList, nameof(ProductViewModel.Id), nameof(ProductViewModel.Name))
        };
        return View(updateViewModel);
    }
    [HttpPost]
    public async Task<IActionResult> EKeyAddPost(EKeyUpdateViewModel eKeyUpdateViewModel)
    {
        EKeyModel ekey = new EKeyModel()
        {
            Admin = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name),
            Key = eKeyUpdateViewModel.EKey.Key,
            ProductId = eKeyUpdateViewModel.ProductId
        };
        await _ekeyRepository.addEkey(ekey);

        return RedirectToAction("EKeys", "Admin" ,new { notification = "E-key was added"  });
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