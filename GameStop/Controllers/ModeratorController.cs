using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;

public class ModeratorController : Controller
{
    private readonly ILogger<ModeratorController> _logger;
    private readonly ApplicationContext _db;
    private readonly IReview _reviewRepository;
    private IEnumerable<ReviewModel> _reviews;
    private readonly IReviewService _reviewService;

    public ModeratorController(ILogger<ModeratorController> logger, ApplicationContext db, IReview reviewRepository, IReviewService reviewService)
    {
        _logger = logger;
        _db = db;
        _reviewRepository = reviewRepository;
        _reviewService = reviewService;
    }
    
    public async Task<IActionResult> Reviews(string? notification)
    {
        var reviews = _reviewRepository.getAll().Include(r => r.Product).ThenInclude(p => p.ProductInfo)
            .Include(r => r.Author).ThenInclude(a=>a.Account).ToList();
        if(notification != null) 
            ModelState.AddModelError("", notification);
        return View(reviews); 
    }
    
    [HttpGet]
    public IActionResult ReviewUpdate(int id)
    {
        var reviews = _reviewRepository.getAll().Include(r => r.Product).ThenInclude(p => p.ProductInfo)
            .Include(r => r.Author).ThenInclude(a=>a.Account).ToList();
        var review = reviews.FirstOrDefault(rev => rev.Id == id);
        
        TempData["AuthorId"] = review.AuthorId;
        TempData["ProductId"] = review.ProductId;
        
        return View(review);
        
    }

    [HttpPost]
    public async Task<IActionResult> ReviewUpdate(ReviewModel review)
    {
        review.AuthorId = (int?)TempData["AuthorId"];
        review.ProductId = (int?)TempData["ProductId"]; 
        _reviewRepository.updateReview(review);

        return RedirectToAction("Reviews", "Moderator", new { notification = "Review was updated"});
    }
}