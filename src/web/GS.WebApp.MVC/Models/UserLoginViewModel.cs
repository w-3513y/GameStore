using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.WebApp.MVC.Models;

public class UserLoginViewModel
{
    [DisplayName("e-mail")]
    [Required(ErrorMessage = "the field {0} is required)")]
    [EmailAddress(ErrorMessage = "{0} invalid")]
    public string Email { get; set; } = "";

    [DisplayName("password")]
    [Required(ErrorMessage = "the field {0} is required")]
    [StringLength(100, ErrorMessage = "the length of the field need to be between {2} and {1}", MinimumLength = 6)]
    public string Password { get; set; } = "";

}