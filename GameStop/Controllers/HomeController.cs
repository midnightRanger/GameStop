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

    public HomeController(ILogger<HomeController> logger, 
        ApplicationContext db, 
        IProductInfo productInfoRepository,
        IProduct productRepository)
    {
        _productInfoRepository = productInfoRepository;
        _productRepository = productRepository; 
        _logger = logger;
        _db = db; 
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
        List<ProductInfoModel> productInfoList;

        if (!String.IsNullOrEmpty(keyword))

            productInfoList = await _productInfoRepository.getAll().Where(p
                => p.Name.Contains(keyword) || p.Name.Contains(keyword)).AsNoTracking().ToListAsync();
        
        else productInfoList = await _productInfoRepository.getProductInfos();
            
            foreach (var productInfo in productInfoList)
            {
                var product = await _productRepository.getAll().FirstOrDefaultAsync(p => p.ProductInfoId == productInfo.Id);
                productView.Add(new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = productInfo.Name,
                        Description = productInfo.Description,
                        Avatar = productInfo.Avatar,
                        Cost = product.Cost,
                        StatusId = product.StatusId, 
                        Developer = productInfo.Developer,
                        ReleaseDate = productInfo.ReleaseDate
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