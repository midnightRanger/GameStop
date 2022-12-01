using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class PaymentInfoModel
{
    [Key]
    public int Id { get; set; }
    public string CardNumber { get; set; } 
    public string Date { get; set; } 
    public int CSV { get; set; }
    public string NameSurname { get; set; }
}