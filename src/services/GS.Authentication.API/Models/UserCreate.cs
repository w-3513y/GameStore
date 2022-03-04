using System.ComponentModel.DataAnnotations;

namespace GS.Authentication.API.Models;

public class UserCreate
{
    [Required(ErrorMessage = "the field {0} is required)")]
    [EmailAddress(ErrorMessage = "{0} invalid")]
    public string Email;

    [Required(ErrorMessage = "the field {0} is required")]
    [EmailAddress(ErrorMessage = "{0} invalid")]
    public string Password;

    [Compare("Password", ErrorMessage = "the password is diferent")]
    public string PasswordConfirmation {get; set;}
}
