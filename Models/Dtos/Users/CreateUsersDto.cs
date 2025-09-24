namespace Project.Models.Dtos.Users;

using System.ComponentModel.DataAnnotations;

public class CreateUsersDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must contain atleast 8 characters")]
    public string Password { get; set; } = string.Empty;
}
