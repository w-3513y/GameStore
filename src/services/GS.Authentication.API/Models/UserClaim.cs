using System.ComponentModel.DataAnnotations;

namespace GS.Authentication.API.Models;

public class UserClaim
{
    public string Value {get; set;} = "";
    public string Type {get; set;} = "";
}