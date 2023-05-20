using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Models.User;

namespace HomeBudget.API.Models.Settlement;

public class Settlement
{
    public UserDto User { get; set; } = new();

    public IReadOnlyCollection<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();

    public decimal Amount { get; set; }

    public bool Receives { get; set; }
}