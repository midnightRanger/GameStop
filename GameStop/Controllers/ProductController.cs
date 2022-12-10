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
    private readonly IUser _userRepository;
    private UserModel _user;
    private int? _ProductId; 

    public ProductController(ILogger<ProductController> logger, 
        ApplicationContext db, 
        IProductInfo productInfoRepository,
        IProduct productRepository, 
        
        ILicense licenseRepository, IReview reviewRepository, IUser userRepository)
    {
        _productInfoRepository = productInfoRepository;
        _productRepository = productRepository;
        _licenseRepository = licenseRepository;
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _logger = logger;
        _allProduct = productRepository.getAll().Include(u => u.Platforms)
            .Include(u=>u.ProductInfo).Include(l=>l.License).Include(r=>r.Reviews).ThenInclude(a=>a.Author).ThenInclude(a=>a.Account).ToList();
        _db = db; 
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(ReviewModel review)
    {
        review.ProductId = (int?)TempData["ProductId"];
        review.IsAccept = true;
        review.Author = _userRepository.getAll().FirstOrDefault(u => u.Account.Login == User.Identity.Name);
        await _reviewRepository.addReview(review);

        return RedirectToAction("ProductInfo", "Product", new { id = review.ProductId});
    }

    [HttpGet]
    public async Task<IActionResult> ProductInfo(int? id)
    {
        ProductModel product = _allProduct.FirstOrDefault(p => p.Id == id);
        ProductViewModel productViewModel = new ProductViewModel()
        {
            Reviews = product.Reviews.Where(r=>r.IsAccept).ToList(),
            Avatar = product.ProductInfo.Avatar,
            Cost = product.Cost,
            Description = product.ProductInfo.Description,
            Developer = product.ProductInfo.Developer,
            Name = product.ProductInfo.Name,
            Id = product.Id,
            license = product.License,
            ReleaseDate = product.ProductInfo.ReleaseDate
        };
        TempData["ProductId"] = id;
        return View(productViewModel); 
    }
}