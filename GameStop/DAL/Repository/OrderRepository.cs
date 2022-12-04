using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class OrderRepository : IOrder
{
    
    private ApplicationContext _db = new();

    public OrderRepository(ApplicationContext db)
    {
        _db = db; 
    }
    
    public async Task addOrder(OrderModel order)
    {
        _db.Order.Add(order);
        await _db.SaveChangesAsync();
    }

    public void updateOrder(OrderModel order)
    {
        _db.Entry(order).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public OrderModel deleteOrder(int id)
    {
        OrderModel? order = _db.Order.Find(id);

        if (order != null)
        {
            _db.Order.Remove(order);
            _db.SaveChanges();
            return order;
        }

        throw new ArgumentNullException();
    }

    public bool checkOrder(int id)
    {
        return _db.Order.Any(o => o.Id == id);
    }

    public async Task<List<OrderModel>> getOrders()
    {
        return await _db.Order.ToListAsync();
    }

    public async Task<OrderModel> getOrder(int id)
    {
        OrderModel? order = await _db.Order.FindAsync(id);

        if (order != null)
        {
            return order;
        }

        throw new ArgumentNullException();
    }

    public IQueryable<OrderModel> getAll()
    {
        return _db.Order;
    }
}