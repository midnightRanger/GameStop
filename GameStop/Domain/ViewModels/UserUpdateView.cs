using System.ComponentModel.DataAnnotations;

namespace GameStop.Models.ViewModels;

public class UserUpdateView
{
    [Required(ErrorMessage = "Fill the login field!")]
    [Display(Name="Login")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Fill the e-mail field!")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Fill the password field!")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Fill in the old password")]
    [DataType(DataType.Password)]
    [Display(Name = "Old Password")]
    public string OldPassword { get; set;}
    
    [Required(ErrorMessage = "Fill the name field!")]
    [DataType(DataType.Text)]
    [Display(Name="Name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Fill the surname field!")]
    [DataType(DataType.Text)]
    [Display(Name="Surname")]
    public string Surname { get; set; }
    
    [Required(ErrorMessage = "Fill the age field!")]
    [Display(Name="Age")]
    public int Age { get; set; }

    public int AccountId { get; set; }
    public int UserId { get; set; }
}