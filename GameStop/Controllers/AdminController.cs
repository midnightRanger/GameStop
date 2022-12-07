using System.Text.Json;
using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;
    private readonly IProduct _productRepository;
    private readonly IEkey _ekeyRepository;
    private IEnumerable<ProductModel> _allProduct;

    public AdminController(ILogger<HomeController> logger, ApplicationContext db, IProduct productRepository, IEnumerable<ProductModel> allProduct, IEkey ekeyRepository)
    {
        _logger = logger;
        _db = db;
        _productRepository = productRepository;
        _ekeyRepository = ekeyRepository;
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
    
    public async Task<IActionResult> EKeys()
    {
        var ekeys = await _ekeyRepository.getAll().Include(e => e.Product).
            ThenInclude(p=>p.ProductInfo).ToListAsync();
        return View(ekeys); 
    }
}