using System.Security.Claims;
using GameStop.Models.ViewModels;
using GameStop.Response;

namespace GameStop.DAL.Interface;

public interface IAccountService
{
    Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

    Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

//    Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
}