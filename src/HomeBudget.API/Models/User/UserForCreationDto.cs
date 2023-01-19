using System.ComponentModel.DataAnnotations;

namespace HomeBudget.API.Models.User;

/// <summary>
/// User object to create a new user
/// </summary>
public class UserForCreationDto
{
    /// <summary>
    /// Name of the user
    /// </summary>
    [Required(ErrorMessage = "You have to provide a name.")]
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Email address of the user
    /// </summary>
    [Required(ErrorMessage = "You have to provide an email address.")]
    [MaxLength(70)]
    public string Email { get; set; } = default!;
    
    /// <summary>
    /// Password of the user
    /// </summary>
    [Required(ErrorMessage = "You have to provide a password.")]
    public string Password { get; set; } = default!;
}