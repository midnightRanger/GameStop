using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;

public class ModeratorController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;
    private readonly IReview _reviewRepository;
    private IEnumerable<ReviewModel> _reviews;

    public ModeratorController(ILogger<HomeController> logger, ApplicationContext db, IReview reviewRepository)
    {
        _logger = logger;
        _db = db;
        _reviewRepository = reviewRepository;
        _reviews = _reviewRepository.getAll().Include(r => r.Product).ThenInclude(p => p.ProductInfo)
            .Include(r => r.Author).ThenInclude(a=>a.Account).ToList();
    }
    
    public async Task<IActionResult> Reviews(string? notification)
    {
        if(notification != null) 
            ModelState.AddModelError("", notification);
        return View(_reviews); 
    }
    
    [HttpGet]
    public IActionResult ReviewUpdate(int id)
    {
        var review = _reviews.FirstOrDefault(rev => rev.Id == id);
        return View(review);
    }
}