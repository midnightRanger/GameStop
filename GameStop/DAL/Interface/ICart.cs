using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface ICart
{
    public Task addCart(CartModel cart);
    public void updateCart(CartModel cart);
    public CartModel deleteCart(in int id);
    
    public bool checkCart(int id);

    public Task<List<CartModel>> getCarts();
    public Task<CartModel> getCart(int id);

    public IQueryable<CartModel> getAll(); 
}