using HomeBudget.API.Models.UserModels;

namespace HomeBudget.API.Models.Transaction;

public class TransactionForUpdateDto
{
    public int Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public UserDto User { get; set; } = new();

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsSettled { get; set; }
}