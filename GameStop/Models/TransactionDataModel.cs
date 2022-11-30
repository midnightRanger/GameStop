using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class TransactionDataModel
{
    [Key]
    public int Id { get; set; }
    public string StartPos { get; set; }
    public string EndPos { get; set; }
    public double TotalKm { get; set; }
    public int TransactionWayId { get; set; }
    public TransactionWayModel? TransactionWay { get; set; }
}