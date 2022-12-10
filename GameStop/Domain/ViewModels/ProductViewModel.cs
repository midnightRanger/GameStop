using System.Collections;

namespace GameStop.Models.ViewModels;

public class ProductViewModel 
{
    public int Id { get; set; }
    public double Cost { get; set; }
    public int StatusId { get; set; }
    public string Description { get; set; }
    public string Developer { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Avatar { get; set; }
    public string Name { get; set;  }
    public LicenseModel? license { get; set; } = null;
    public PublisherModel? publisher { get; set; } = null; 
    public List<PlatformModel>? Platforms { get; set; }
    public List<ReviewModel>? Reviews { get; set; }

}