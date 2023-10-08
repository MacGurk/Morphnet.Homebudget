using AutoMapper;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Query.Transaction;

public record GetTransactionQuery(int TransactionId) : IRequest<TransactionDto?>;

public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, TransactionDto?>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IMapper mapper;

    public GetTransactionQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        this.transactionRepository = transactionRepository;
        this.mapper = mapper;
    }

    public async Task<TransactionDto?> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.GetTransactionByIdAsync(request.TransactionId);

        return mapper.Map<TransactionDto>(transactions);
    }
}
