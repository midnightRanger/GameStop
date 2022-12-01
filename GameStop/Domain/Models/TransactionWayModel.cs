
using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class TransactionWayModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public double TotalCost { get; set; }
}