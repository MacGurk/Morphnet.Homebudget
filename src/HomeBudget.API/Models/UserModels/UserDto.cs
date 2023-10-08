namespace HomeBudget.API.Models.UserModels;

/// <summary>
/// Full user object
/// </summary>
public class UserDto
{
    /// <summary>
    /// Id of the user
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the user
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the user
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Is the user a contributor
    /// </summary>
    public bool IsContributor { get; set; }
}