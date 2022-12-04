namespace GameStop.Models.ViewModels;

public class OrderViewModel
{
    public DateTime DateTime { get; set; } 
    public double Sum { get; set; } 
    public UserModel User { get; set; } 
    public List<EKeyModel> Ekeys { get; set; } 
    public CartModel Cart { get; set; } 
}