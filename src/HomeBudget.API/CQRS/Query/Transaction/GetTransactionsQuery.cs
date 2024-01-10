using AutoMapper;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Services.Repositories;
using HomeBudget.API.Services.Utils;
using MediatR;

namespace HomeBudget.API.CQRS.Query.Transaction;

public record GetTransactionsQuery(
    string? SearchQuery,
    int? Month,
    int? Year
) : IRequest<IEnumerable<TransactionDto>>;

public class
    GetTransactionsQueryRequestHandler : IRequestHandler<GetTransactionsQuery, IEnumerable<TransactionDto>>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IMapper mapper;

    public GetTransactionsQueryRequestHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        this.transactionRepository = transactionRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.GetTransactionsAsync(
            request.SearchQuery,
            request.Month,
            request.Year
        );

        return mapper.Map<IEnumerable<TransactionDto>>(transactions);
    }
}