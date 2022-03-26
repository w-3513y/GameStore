using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.WebApp.MVC.Models;

public class UserCreate
{
    [DisplayName("e-mail")]
    [Required(ErrorMessage = "the field {0} is required)")]
    [EmailAddress(ErrorMessage = "{0} invalid")]
    public string Email { get; set; } = "";

    [DisplayName("password")]
    [Required(ErrorMessage = "the field {0} is required")]
    [EmailAddress(ErrorMessage = "{0} invalid")]
    public string Password { get; set; } = "";

    [DisplayName("confirm your password")]
    [Compare("Password", ErrorMessage = "the password is diferent")]
    public string PasswordConfirmation { get; set; } = "";
}
