using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStop.Controllers;

public class ProductController: Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly ApplicationContext _db; 
    private readonly IProductInfo _productInfoRepository;
    private readonly IProduct _productRepository;
    private readonly ILicense _licenseRepository; 

    public ProductController(ILogger<ProductController> logger, 
        ApplicationContext db, 
        IProductInfo productInfoRepository,
        IProduct productRepository, 
        ILicense licenseRepository)
    {
        _productInfoRepository = productInfoRepository;
        _productRepository = productRepository;
        _licenseRepository = licenseRepository;
        _logger = logger;
        _db = db; 
    }

    [HttpGet]
    public async Task<IActionResult> ProductInfo(int? id)
    {
        ProductModel product = _productRepository.getProduct(id);
        ProductInfoModel productInfo = await _productInfoRepository.getProductInfo(product.ProductInfoId);
        LicenseModel licenseModel = await _licenseRepository.getLicense(product.LicenseId);

        ProductViewModel productViewModel = new ProductViewModel()
        {
            Avatar = productInfo.Avatar,
            Cost = product.Cost,
            Description = productInfo.Description,
            Developer = productInfo.Developer,
            Name = productInfo.Name,
            Id = product.Id,
            license = licenseModel,
            ReleaseDate = productInfo.ReleaseDate
        };
        
        return View(productViewModel); 
    }
}