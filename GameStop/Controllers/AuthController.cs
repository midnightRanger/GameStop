using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace GameStop.Controllers;

public class AuthController : Controller
{
    private readonly ApplicationContext db;
    private readonly IWebHostEnvironment _app;
    private readonly IAccountService _accountService;

    private readonly ILogger<AuthController> _logger;
    
    public AuthController(ApplicationContext context, IWebHostEnvironment app, IAccountService accountService)
    {
        db = context;
        _app = app;
        _accountService = accountService; 
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.Login(model);
            if (response.StatusCode == GameStop.StatusCode.OK)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Main", "Home");
            }
            ModelState.AddModelError("", response.Description);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(); 
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(ModelState.IsValid)
        {
            var response = await _accountService.Register(model);
            if (response.StatusCode == GameStop.StatusCode.OK)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", response.Description);
        }
        return View(model);
    }
}