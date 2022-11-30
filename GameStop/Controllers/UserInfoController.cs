using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers;

public class UserInfoController: Controller
{
    
    private readonly ILogger<UserInfoController> _logger;
    private readonly ApplicationContext _db;

    private readonly IAccount _accountRepository; 
    private readonly IUser _userRepository;

    public UserInfoController(ILogger<UserInfoController> logger, 
        ApplicationContext db, 
        IProductInfo productInfoRepository,
        IUser userRepository,
        IAccount accountRepository
        )
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository; 
        _logger = logger;
        _db = db; 
    }

    [HttpGet]
    public async Task<IActionResult> UserUpdate()
    {
       
        AccountModel account = await _accountRepository.getAll().FirstOrDefaultAsync(u=>u.Login == User.Identity.Name);
        UserModel user = await _userRepository.getAll().FirstOrDefaultAsync(u => u.AccountId == account.Id);

        UserViewModel userView = new UserViewModel()
        {
            AccountId = account.Id,
            Age = user.Age, Email = account.Email,
            Login = account.Login, Name = user.Name, Surname = user.Surname, Password = account.Password,
            UserId = user.Id,
            PasswordConfirm = account.Password
        };
        
        return View(userView); 
    }

    [HttpPost]
    public async Task<IActionResult> UserUpdate(UserViewModel userView)
    {
        if(ModelState.IsValid)
        {
            // var response = await _accountService.Register(model);
            // if (response.StatusCode == GameStop.StatusCode.OK)
            // {
            //     return RedirectToAction("Index", "Home");
            // }
            // ModelState.AddModelError("", response.Description);
        }
        return View(userView);
    }
}