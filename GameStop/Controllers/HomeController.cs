using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using GameStop.DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;

namespace GameStop.Controllers;

public class HomeController : Controller
{
   
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db; 
    private readonly IProductInfo _productInfoRepository;
    private readonly IProduct _productRepository;
    private IEnumerable<ProductModel> _allProduct; 

    public HomeController(ILogger<HomeController> logger, 
        ApplicationContext db, 
        IProductInfo productInfoRepository,
        IProduct productRepository
        )
    {
        _productInfoRepository = productInfoRepository;
        _productRepository = productRepository; 
        _logger = logger;
        _db = db;
        _allProduct = productRepository.getAll().Include(u => u.Platforms).Include(u=>u.ProductInfo).ToList();

    }
    
    public IActionResult Index()
    {
        return View();

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Main(string keyword)
    {
        ViewData["keyword"] = keyword;
        
        List<ProductViewModel> productView = new List<ProductViewModel>();
        IEnumerable<ProductModel> productList;

        if (!String.IsNullOrEmpty(keyword))
            productList = _allProduct.Where(p
                => p.ProductInfo.Name.Contains(keyword) || p.ProductInfo.Name.Contains(keyword));
        else productList = _allProduct;
        
        foreach (var product in productList)
            {
                productView.Add(new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.ProductInfo.Name,
                        Description = product.ProductInfo.Description,
                        Avatar = product.ProductInfo.Avatar,
                        Cost = product.Cost,
                        StatusId = product.StatusId, 
                        Developer = product.ProductInfo.Developer,
                        ReleaseDate = product.ProductInfo.ReleaseDate,
                        Platforms = product.Platforms
                    }
                );
            }
            return View(productView);
    }
 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}