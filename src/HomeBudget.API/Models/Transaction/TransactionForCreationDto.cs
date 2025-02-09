using System.ComponentModel.DataAnnotations;

namespace HomeBudget.API.Models.Transaction;

/// <summary>
/// Transaction object to create a new user
/// </summary>
public class TransactionForCreationDto
{
    /// <summary>
    /// Date of the transaction
    /// </summary>
    [Required]
    public DateOnly Date { get; set; }

    /// <summary>
    /// Associated user of the transaction
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// A description of the transaction
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The price of the transaction
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }
}