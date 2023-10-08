using AutoMapper;
using HomeBudget.API.Models.Settlement;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Models.UserModels;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Query.Settlement;

public record GetSettlementsQuery : IRequest<IEnumerable<SettlementDto>>;

public class GetSettlementsQueryHandler : IRequestHandler<GetSettlementsQuery, IEnumerable<SettlementDto>>
{
    private readonly IUserRepository userRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly IMapper mapper;

    public GetSettlementsQueryHandler(IUserRepository userRepository, ITransactionRepository transactionRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.transactionRepository = transactionRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<SettlementDto>> Handle(GetSettlementsQuery request, CancellationToken cancellationToken)
    {
        var unsettledTransactions = (await transactionRepository.GetUnsettledTransactionsAsync()).ToList();
        var users = (await userRepository.GetUsersAsync(isContributorFilter: true)).ToList();

        var splitTotalPrice = unsettledTransactions.Sum(t => t.Price) / users.Count();
        var userGroups = unsettledTransactions.GroupBy(t => t.User.Id).ToList();
        
        var settlements = new List<SettlementDto>();
        foreach (var user in users)
        {
            var group = userGroups.SingleOrDefault(x => x.Key == user.Id);
            var userDto = mapper.Map<UserDto>(user);
            if (group is null)
            {
                settlements.Add(
                    new SettlementDto
                    {
                        User = userDto,
                        Transactions = new List<TransactionDto>(),
                        Amount = splitTotalPrice,
                        Receives = false
                    });
                continue;
            }

            var transactions = group.ToList();
            var totalUserPrice = transactions.Sum(t => t.Price);
            settlements.Add(
                new SettlementDto
                {
                    User = userDto,
                    Transactions = mapper.Map<IReadOnlyCollection<TransactionDto>>(transactions),
                    Amount = Math.Abs(totalUserPrice - splitTotalPrice),
                    Receives = totalUserPrice >= splitTotalPrice
                });
            
        }

        return settlements;
    }
}
