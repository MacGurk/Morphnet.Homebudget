namespace HomeBudget.API.Models.UserModels;

public class UserForUpdatePasswordDto
{
    /// <summary>
    /// Id of the user to update
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Updates password for User
    /// </summary>
    public string Password { get; set; } = string.Empty;
}