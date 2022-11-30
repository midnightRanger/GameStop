using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class ProductModel
{
    [Key]
    public int Id { get; set; }
    public int StatusId { get; set; }
    public ProductStatusModel? Status { get; set; }
    public double Cost { get; set; }
    
    public int LicenseId { get; set; }
    public LicenseModel? License { get; set; }
    
    public string EKeyId { get; set; }
    public EKeyModel? EKey { get; set; }
    
    public int ProductInfoId { get; set; }
    public ProductInfoModel? ProductInfo { get; set; }

    public List<PlatformModel> Platforms { get; set; } = new List<PlatformModel>();
    public List<OrderModel> Orders { get; set; } = new List<OrderModel>();
    public List<ConfirmedOrder> ConfirmedOrders { get; set; } = new List<ConfirmedOrder>();
}