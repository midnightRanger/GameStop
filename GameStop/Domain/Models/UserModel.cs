using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GameStop.Models;

public class UserModel 
{
    [Key]
    public int Id { get; set; }
    public string? Surname { get; set; } 
    public string? Name { get; set; } 
    public int Age { get; set; } 
    public int Points { get; set; }
    public String? Avatar { get; set; }
    
    public int AccountId { get; set; } 
    public AccountModel? Account { get; set; }
    public double? Balance { get; set; } = null; 
    public List<CartModel>? Cart { get; set; }
    
    public List<ReviewModel>? Reviews { get; set; } = new();
}