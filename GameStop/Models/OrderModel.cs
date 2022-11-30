using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class OrderModel
{
    [Key]
    public int OrderId { get; set; }
    public string DateTime { get; set; }
    public double Sum { get; set; }
    public int UserId { get; set; }
    public UserModel? User { get; set; }
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    public List<ConfirmedOrder> ConfirmedOrders { get; set; } = new List<ConfirmedOrder>();
}