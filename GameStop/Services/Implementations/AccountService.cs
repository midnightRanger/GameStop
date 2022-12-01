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
    private readonly ILogger<AccountService> _logger; 
    
    public AccountService(IAccount accountRepository,
        ILogger<AccountService> logger, IUser userRepository)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _logger = logger;
        
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
                Password = PasswordHasher.HashPassword(model.Password),
            };

            var userInfo = new UserModel()
            {
                Name = model.Name,
                Surname = model.Surname,
                AccountId = user.Id,
                Account = user,
                Age = model.Age
            };

            //TODO 
            // var profile = new Profile()
            // {
            //     UserId = user.Id,
            // };

            await _accountRepository.addAccount(user);
            await _userRepository.addUser(userInfo);
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
            new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Email)
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}