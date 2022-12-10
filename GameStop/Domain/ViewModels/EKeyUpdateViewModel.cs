using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStop.Models.ViewModels;

public class EKeyUpdateViewModel
{
    public int? Number { get; set; }
    public EKeyModel? EKey { get; set; }
    public UserModel? Admin { get; set; }
    public int? ProductId { get; set; }
    public ProductModel? Product { get; set; }
    public SelectList Products { get; set; }
}