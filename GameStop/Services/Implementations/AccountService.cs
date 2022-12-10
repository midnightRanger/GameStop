using System.Security.Claims;
using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Models.Safety;
using GameStop.Models.ViewModels;
using GameStop.Response;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Services;

public class AccountService : IAccountService
{
    private readonly IAccount _accountRepository;
    private readonly IUser _userRepository;
    private readonly ICart _cartRepository; 
    private readonly ILogger<AccountService> _logger; 
    
    public AccountService(IAccount accountRepository,
        ILogger<AccountService> logger, IUser userRepository, ICart cartRepository)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _logger = logger;
        _cartRepository = cartRepository;

        //TODO
        //_proFileRepository = proFileRepository;
    }


    public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
    {
        
        try
        {
            var user = await _accountRepository.getAll().FirstOrDefaultAsync(x => x.Login == model.Login);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь с таким логином уже есть",
                };
            }

            user = new AccountModel()
            {
                Login = model.Login,
                Email = model.Email,
                Role = "USER",
                IsActive = true,
                Password = PasswordHasher.HashPassword(model.Password),
            };

            var userInfo = new UserModel()
            {
                Name = model.Name,
                Surname = model.Surname,
                AccountId = user.Id,
                Account = user,
                Age = model.Age,
                Avatar = "~/avatars/default.png",
                Balance = 0.00
            };

            var userCart = new CartModel()
            {
                OwnerId = userInfo.Id, 
                Sum = 0.00, 
                Owner = userInfo
            };

            //TODO 
            // var profile = new Profile()
            // {
            //     UserId = user.Id,
            // };

            await _accountRepository.addAccount(user);
            await _userRepository.addUser(userInfo);
            await _cartRepository.addCart(userCart);
           //await _proFileRepository.Create(profile);
            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Объект добавился",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Register]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    //TODO make update method 
    public async Task<BaseResponse<ClaimsIdentity>> Update(UserViewModel user)
    {
        try {
         var account = await _accountRepository.getAll().FirstOrDefaultAsync(x => x.Login == user.Login);
        if (user == null)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = "Такого пользователя не существует",
            };
        }
        //
        // user = new AccountModel()
        // {
        //     Login = model.Login,
        //     Email = model.Email,
        //     Password = PasswordHasher.HashPassword(model.Password),
        // };
        //
        // var userInfo = new UserModel()
        // {
        //     Name = model.Name,
        //     Surname = model.Surname,
        //     AccountId = user.Id,
        //     Account = user,
        //     Age = model.Age
        // };

        //TODO 
        // var profile = new Profile()
        // {
        //     UserId = user.Id,
        // };

        //await _accountRepository.addAccount(user);
        //await _userRepository.addUser(userInfo);
        //await _proFileRepository.Create(profile);
        //var result = Authenticate(user);

        return new BaseResponse<ClaimsIdentity>()
        {
           // Data = result,
            Description = "Объект добавился",
            StatusCode = StatusCode.OK
        };
    }


    catch (Exception ex)
        {
            _logger.LogError(ex, $"[Update]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<bool>> ChangeAccount(UserUpdateView model)
    {
        try
        {
            var user = await _userRepository.getAll().FirstOrDefaultAsync(x => x.Id == model.UserId);
            var account = await _accountRepository.getAll().FirstOrDefaultAsync(x => x.Id == model.AccountId);
            if (user == null || account == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "User or User's account not found"
                };
            }

            /*
             * Account constructor
             */
            account.Email = model.Email;
            account.Login = model.Login;
            account.Role = "USER";
       
            account.Password = PasswordHasher.HashPassword(model.Password);

            /*
             * User constructor
            */
            user.Account = account;
            user.Age = model.Age;
            //TODO add Avatar change
            user.Name = model.Name;
            user.Surname = model.Surname;

            _userRepository.updateUser(user);
            _accountRepository.updateAccount(account);
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Account was updated"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[UpdateUser]: {ex.Message}");
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
    {
        try
        {
            var user = await _accountRepository.getAll().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь не найден"
                };
            }

            if (user.Password != PasswordHasher.HashPassword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный пароль или логин"
                };
            }
            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Login]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    
    
    private ClaimsIdentity Authenticate(AccountModel account)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role)
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}