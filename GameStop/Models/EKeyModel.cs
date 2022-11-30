using System.ComponentModel.DataAnnotations;

namespace GameStop.Models;

public class EKeyModel
{
    [Key]
    public string Key { get; set; }
    public int? InsertedBy { get; set; }
    public AdminModel Admin { get; set; }
}