using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Command.Settlement;

public record SettleTransactionsCommand(List<int> TransactionIds) : IRequest;

public class SettleTransactionsCommandHandler : IRequestHandler<SettleTransactionsCommand>
{
    private readonly ITransactionRepository transactionRepository;

    public SettleTransactionsCommandHandler(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public async Task Handle(SettleTransactionsCommand request, CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.GetTransactionsByIdAsync(request.TransactionIds);
        foreach (var transaction in transactions)
        {
            transaction.IsSettled = true;
        }

        await transactionRepository.SaveChangedAsync();
    }
}