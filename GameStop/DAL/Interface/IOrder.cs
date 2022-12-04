using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface IOrder
{
    public Task addOrder(OrderModel order);
    public void updateOrder(OrderModel order);
    public OrderModel deleteOrder(int id);
    
    public bool checkOrder(int id);

    public Task<List<OrderModel>> getOrders();
    public Task<OrderModel> getOrder(int id);

    public IQueryable<OrderModel> getAll(); 
}