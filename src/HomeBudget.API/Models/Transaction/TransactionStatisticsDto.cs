using HomeBudget.API.Entities;
using HomeBudget.API.Models.UserModels;

namespace HomeBudget.API.Models.Transaction;

public class TransactionStatisticsDto
{
    public UserDto User { get; set; } = new();
    public decimal[] MonthlyTotal { get; set; } = new decimal[12];
}