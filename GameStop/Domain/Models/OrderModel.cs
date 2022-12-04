using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class OrderModel
{
    [Key]
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public double Sum { get; set; }
    public int UserId { get; set; }
    public UserModel? User { get; set; }
    public int? TransactionDataId { get; set; }
    public TransactionDataModel? TransactionDataModel;
    
    public List<EKeyModel> EKeys { get; set; } = new();
}