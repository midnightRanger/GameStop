using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class AdminModel
{
    [Key]
    public int Id { get; set; }
    public int Lvl { get; set; } 
    public int AccountId { get; set; } 
    public AccountModel? Account { get; set; } 
}