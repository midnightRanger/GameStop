using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class ProductInfoModel
{
    [Key]
    public int Id { get; set;  }
    public string Description { get; set; }
    public string Developer { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Avatar { get; set; }
    public string Name { get; set; }
}