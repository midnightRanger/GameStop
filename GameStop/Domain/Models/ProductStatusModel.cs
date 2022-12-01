using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class ProductStatusModel
{
    [Key] 
    public int Id { get; set; }
    public string Status { get; set; } = "N/A";
}