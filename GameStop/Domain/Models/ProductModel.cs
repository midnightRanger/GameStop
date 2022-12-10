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

    public int ProductInfoId { get; set; }
    public ProductInfoModel? ProductInfo { get; set; }
    
    public List<PlatformModel>? Platforms { get; set; } = new();
    public List<ReviewModel>? Reviews { get; set; } = new();
}