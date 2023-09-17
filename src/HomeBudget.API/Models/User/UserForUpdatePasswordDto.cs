namespace HomeBudget.API.Models.User;

public class UserForUpdatePasswordDto
{
    /// <summary>
    /// Id of the user to update
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Updates password for User
    /// </summary>
    public string Password { get; set; } = default!;
}