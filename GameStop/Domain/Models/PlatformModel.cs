
using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class PlatformModel
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set;}

    public List<ProductModel> Products { get; set; } = new(); 
}