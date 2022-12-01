using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GameStop.Models;

public class AccountModel
{
    //TODO VALIDATION
    [Key]
    public int Id { get; set; } 
    public string Login { get; set; } 
    public string Email { get; set; } 
    public string Password { get; set; }
    
    public bool IsActive { get; set; }

    public UserModel? User { get; set; }
    
    
}