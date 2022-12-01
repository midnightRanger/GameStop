using GameStop.DAL.Interface;
using GameStop.DAL.Repository;
using GameStop.Models;
using GameStop.Models.Safety;
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
    private List<UserModel> _userList;
    private UserModel _user;

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
        _userList = userRepository.getAll().Include(u => u.Account).ToList();
        
    }
    
    [HttpGet]
    public async Task<IActionResult> UserInfo()
    {
        _user = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name);
       // var user = _userRepository.getAll().Include(u => u.Account).ToList();
        return View(_user);
    }

    [HttpGet]
    public async Task<IActionResult> UserUpdate()
    {
        AccountModel account = await _accountRepository.getAll().FirstOrDefaultAsync(u=>u.Login == User.Identity.Name);
        UserModel user = await _userRepository.getAll().FirstOrDefaultAsync(u => u.AccountId == account.Id);

        UserUpdateView userView = new UserUpdateView()
        {
            AccountId = account.Id,
            Age = user.Age, Email = account.Email,
            Login = account.Login, Name = user.Name, Surname = account.User.Surname, Password = account.Password,
            UserId = user.Id,
            OldPassword = account.Password
        };
        
        return View(userView); 
    }

    [HttpPost]
    public async Task<IActionResult> UserUpdate(UserUpdateView userView)
    {
        _user = _userList.FirstOrDefault(u => u.Account?.Login == User.Identity.Name);
        
        if (PasswordHasher.HashPassword(userView.OldPassword) !=
            _user.Account.Password)
            
            ModelState.AddModelError("OldPassword", "Your old password is incorrect! " );

        if(ModelState.IsValid)
        {
                return Ok();
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