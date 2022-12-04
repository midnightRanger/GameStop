namespace GameStop.Models.ViewModels;

public class OrderViewModel
{
    public DateTime DateTime;
    public double Sum;
    public UserModel User;
    public List<EKeyModel> Ekeys;
    public CartModel Cart;
}