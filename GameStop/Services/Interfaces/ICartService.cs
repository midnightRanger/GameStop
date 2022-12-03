using GameStop.Models;
using GameStop.Response;

namespace GameStop.DAL.Interface;

public interface ICartService
{
    Task<BaseResponse<bool>> AddToCart(int? productId, UserModel _user);
}