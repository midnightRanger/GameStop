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

    public ModeratorController(ILogger<HomeController> logger, ApplicationContext db, IReview reviewRepository)
    {
        _logger = logger;
        _db = db;
        _reviewRepository = reviewRepository;
    }
    
    public async Task<IActionResult> Reviews(string? notification)
    {
        if(notification != null) 
            ModelState.AddModelError("", notification);
        var reviews = await _reviewRepository.getAll().Include(r => r.Product).ThenInclude(p => p.ProductInfo)
            .Include(r => r.Author).ToListAsync();
        return View(reviews); 
    }
}