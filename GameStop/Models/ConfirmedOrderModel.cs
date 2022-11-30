using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class ConfirmedOrder
{
    [Key] 
    public int Id { get; set; }
    public int OrderId { get; set; }
    public OrderModel Order { get; set; }
    
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }
    
    public int TransactionDataId { get; set; }
    public TransactionDataModel TransactionData { get; set; }
    
}