using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStop.Models;

public class EKeyModel
{
    [Key]
    public string? Key { get; set; }
    public int? AdminId { get; set; }
    public UserModel? Admin { get; set; }
    public int? ProductId { get; set; }
    public ProductModel? Product { get; set; }
    public int? CartId { get; set; }
    public CartModel? Cart { get; set; }
    public int? OrderId { get; set; }
    public OrderModel? Order { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Number { get; set; }
}