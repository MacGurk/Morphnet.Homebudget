using AutoMapper;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Query.Transaction;

public record GetTransactionStatisticsQuery(int Year) : IRequest<IEnumerable<TransactionStatisticsDto>>;

public class GetTransactionStatisticsQueryHandler : IRequestHandler<GetTransactionStatisticsQuery, IEnumerable<TransactionStatisticsDto>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public GetTransactionStatisticsQueryHandler(ITransactionRepository transactionRepository, IUserRepository userRepository, IMapper mapper)
    {
        this._transactionRepository = transactionRepository;
        this._userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransactionStatisticsDto>> Handle(GetTransactionStatisticsQuery request, CancellationToken cancellationToken)
    {
        var contributingUsers = await _userRepository.GetUsersAsync(isContributorFilter: true);
        var transactions = (await _transactionRepository.GetTransactionsAsync(year: request.Year, month: null, searchQuery: null)).ToList();

        var statistics = new List<TransactionStatistics>();

        foreach (var user in contributingUsers)
        {
            var userStatistics = new TransactionStatistics(user);
            for (int i = 0; i < 12; i++)
            {
                var sum = transactions.Where(x => x.Date.Month == i + 1 && x.User.Id == user.Id).Sum(x => x.Price);
                userStatistics.MonthlyTotal[i] = sum;
            }

            statistics.Add(userStatistics);
        }
        
        return _mapper.Map<IEnumerable<TransactionStatisticsDto>>(statistics);
    }
}