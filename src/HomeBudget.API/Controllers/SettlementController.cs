using AutoMapper;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.Settlement;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Models.User;
using HomeBudget.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers;

[Route("api/v{version:apiVersion}")]
[ApiVersion("1")]
[ApiController]
public class SettlementController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public SettlementController(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Settlement>>> GetSettlementAsync()
    {
        var unsettledTransaction = await _transactionRepository.GetUnsettledTransactionsAsync();

        var totalUnsettledPrice = unsettledTransaction.Sum(t => t.Price);

        var userGroups = unsettledTransaction.GroupBy(t => t.User);

        // We should use total Users here instead of the count of users with transactions
        var averageUnsettledPrice = totalUnsettledPrice / userGroups.Count();
        
        var settlements = new List<Settlement>();

        // Also we should iterate over all Users
        foreach (var group in userGroups)
        {
            var totalPrice = group.Sum(x => x.Price);
            var settlement = new Settlement
            {
                User = _mapper.Map<UserDto>(group.Key),
                Transactions = _mapper.Map<IReadOnlyCollection<TransactionDto>>(group.ToList()),
                Amount = Math.Abs(totalPrice - averageUnsettledPrice),
                Receives = totalPrice >= averageUnsettledPrice
            };
            
            settlements.Add(settlement);
        }
        
        return Ok(settlements);
    }
}