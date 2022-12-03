using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class CartRepository : ICart
{
    
    private ApplicationContext _db = new();

    public CartRepository(ApplicationContext db)
    {
        _db = db; 
    }
    
    public async Task addCart(CartModel cart)
    {
        _db.Cart.Add(cart);
        await _db.SaveChangesAsync();
    }

    public void updateCart(CartModel cart)
    {
        _db.Entry(cart).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public CartModel deleteCart(in int id)
    {
        CartModel? cart = _db.Cart.Find(id);

        if (cart != null)
        {
            _db.Cart.Remove(cart);
            _db.SaveChanges();
            return cart;
        }

        throw new ArgumentNullException();
    }

    public bool checkCart(int id)
    {
        return _db.Cart.Any(c => c.Id == id);
    }

    public async Task<List<CartModel>> getCarts()
    {
        return await _db.Cart.ToListAsync();
    }

    public async Task<CartModel> getCart(int id)
    {
        CartModel? cart = await _db.Cart.FindAsync(id);

        if (cart != null)
        {
            return cart;
        }

        throw new ArgumentNullException();
    }

    public IQueryable<CartModel> getAll()
    {
        return _db.Cart; 
    }
}