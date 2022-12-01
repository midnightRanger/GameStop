using System.ComponentModel.DataAnnotations;

namespace GameStop.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Укажите электронную почту")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Укажите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}