using GameStop.Models;
using GameStop.Response;

namespace GameStop.DAL.Interface;

public interface IOrderService
{
    Task<BaseResponse<bool>> MakeOrder(UserModel user, CartModel cart);
}