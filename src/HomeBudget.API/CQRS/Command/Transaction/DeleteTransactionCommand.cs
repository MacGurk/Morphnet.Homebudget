using HomeBudget.API.CQRS.Events;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Command.Transaction;

public record DeleteTransactionCommand(int TransactionId) : IRequest<IEvent>;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, IEvent>
{
    private readonly ITransactionRepository transactionRepository;

    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public async Task<IEvent> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.GetTransactionByIdAsync(request.TransactionId);
        if (transaction is null)
        {
            return new TransactionNotFoundEvent();
        }
        
        transactionRepository.DeleteTransaction(transaction);
        await transactionRepository.SaveChangedAsync();
        
        return new TransactionDeletedEvent();
    }
}