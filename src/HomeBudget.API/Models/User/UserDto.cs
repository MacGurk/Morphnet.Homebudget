namespace HomeBudget.API.Models.User;

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
    public string Name { get; set; } = default!;

    /// <summary>
    /// Email address of the user
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Is the user a contributor
    /// </summary>
    public bool IsContributor { get; set; }
}