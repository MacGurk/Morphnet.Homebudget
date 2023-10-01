using HomeBudget.API.Models.UserModels;

namespace HomeBudget.API.Models.Transaction;

/// <summary>
/// Full Transaction object
/// </summary>
public class TransactionDto
{
    public int Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public UserDto User { get; set; } = null!;

    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public bool IsSettled { get; set; }
}