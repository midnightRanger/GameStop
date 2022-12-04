using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Response;

namespace GameStop.Services;

public class CartService : ICartService
{
    private readonly ICart _cartRepository;
    private readonly IEkey _ekeyRepository;
    private readonly ILogger<AccountService> _logger;

    public CartService(IEkey ekeyRepository, ICart cartRepository, ILogger<AccountService> logger)
    {
        _ekeyRepository = ekeyRepository;
        _cartRepository = cartRepository;
        _logger = logger;
    }

    public async Task<BaseResponse<bool>> DeleteFromCart(int? productId, CartModel cart)
    {
        try
        { 
            EKeyModel ekey = cart.Ekeys.FirstOrDefault(e => e.ProductId == productId);

            if (ekey == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.KeyNotFound,
                    Description = "There is no key according to selected product"
                };
                
            }

            ekey.CartId = null; 
            await _ekeyRepository.updateEkey(ekey);
            
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Item was deleted from the cart"
            };
        }   
        
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[DeleteFromCart]: {ex.Message}");
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<bool>> AddToCart(int? productId, UserModel _user)
    {
        try
        {
            EKeyModel ekey = _ekeyRepository.getAll().FirstOrDefault(k => k.ProductId == productId
                                                                          && k.CartId == null && k.OrderId == null);
            CartModel cart = _cartRepository.getAll().FirstOrDefault(c => c.OwnerId == _user.Id);

            if (ekey == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.KeyNotFound,
                    Description = "Sorry, but we dont have the E-Key for this product at this time"
                };
            }

            if (cart == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.CartNotFound,
                    Description = "You dont have a cart entity. WTF?"
                };
            }
            
            
            ekey.CartId = cart.Id;
           await _ekeyRepository.updateEkey(ekey);
            
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Item was added to the cart"
            };
            
        }
        
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[AddToCart]: {ex.Message}");
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}