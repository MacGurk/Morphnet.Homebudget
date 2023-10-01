﻿using System.ComponentModel.DataAnnotations;

namespace HomeBudget.API.Models.UserModels;

public class UserForUpdateDto
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
    
    /// <summary>
    /// Is the user a contributor
    /// </summary>
    public bool IsContributor { get; set; } = true;
}