using System.ComponentModel.DataAnnotations;

namespace GS.Authentication.API.Models;

public class UserResponse
{
    public string AccessToken {get; set;}
    public double ExpiresIn {get; set;}

    public UserToken userToken {get; set;}

}