using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;

public class ProductController: Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly ApplicationContext _db; 
    private readonly IProductInfo _productInfoRepository;
    private readonly IProduct _productRepository;
    private readonly ILicense _licenseRepository;
    private readonly IReview _reviewRepository;
    private IEnumerable<ProductModel> _allProduct; 
    private UserModel _user;

    public ProductController(ILogger<ProductController> logger, 
        ApplicationContext db, 
        IProductInfo productInfoRepository,
        IProduct productRepository, 
        
        ILicense licenseRepository, IReview reviewRepository)
    {
        _productInfoRepository = productInfoRepository;
        _productRepository = productRepository;
        _licenseRepository = licenseRepository;
        _reviewRepository = reviewRepository;
        _logger = logger;
        _allProduct = productRepository.getAll().Include(u => u.Platforms)
            .Include(u=>u.ProductInfo).Include(l=>l.License).Include(r=>r.Reviews).ThenInclude(a=>a.Author).ThenInclude(a=>a.Account).ToList();
        _db = db; 
    }

    [HttpGet]
    public async Task<IActionResult> ProductInfo(int? id)
    {
        ProductModel product = _allProduct.FirstOrDefault(p => p.Id == id);
        ProductViewModel productViewModel = new ProductViewModel()
        {
            Reviews = product.Reviews,
            Avatar = product.ProductInfo.Avatar,
            Cost = product.Cost,
            Description = product.ProductInfo.Description,
            Developer = product.ProductInfo.Developer,
            Name = product.ProductInfo.Name,
            Id = product.Id,
            license = product.License,
            ReleaseDate = product.ProductInfo.ReleaseDate
        };
        
        return View(productViewModel); 
    }
}