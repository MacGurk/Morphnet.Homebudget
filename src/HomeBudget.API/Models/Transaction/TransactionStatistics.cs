using HomeBudget.API.Entities;

namespace HomeBudget.API.Models.Transaction;

public class TransactionStatistics
{
    public TransactionStatistics(User user)
    {
        MonthlyTotal = new decimal[12];
        User = user;
    }

    public User User { get; set; }
    public decimal[] MonthlyTotal { get; set; }
}