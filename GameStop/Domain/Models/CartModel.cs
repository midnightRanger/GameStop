using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class CartModel
{
    [Key]
    public int Id { get; set; }
    
    public int? OwnerId { get; set; }
    public UserModel Owner { get; set; }
    public double Sum { get; set;  }
    public List<EKeyModel> Ekeys { get; set; } = new();
}