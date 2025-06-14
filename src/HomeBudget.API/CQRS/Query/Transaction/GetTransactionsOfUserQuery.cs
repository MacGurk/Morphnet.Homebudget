using AutoMapper;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Query.Transaction;

public record GetTransactionsOfUserQuery(int UserId) : IRequest<IEnumerable<TransactionDto>?>;

public class GetTransactionsOfUserQueryRequestHandler : IRequestHandler<GetTransactionsOfUserQuery, IEnumerable<TransactionDto>?>
{
    private readonly IUserRepository userRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly IMapper mapper;

    public GetTransactionsOfUserQueryRequestHandler(IUserRepository userRepository, ITransactionRepository transactionRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.transactionRepository = transactionRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<TransactionDto>?> Handle(GetTransactionsOfUserQuery request, CancellationToken cancellationToken)
    {
        var userExist = await userRepository.UserExistsAsync(request.UserId);
        if (!userExist)
        {
            return null;
        }
        
        return mapper.Map<IEnumerable<TransactionDto>>(await transactionRepository.GetTransactionsByUserAsync(request.UserId));
    }
}


