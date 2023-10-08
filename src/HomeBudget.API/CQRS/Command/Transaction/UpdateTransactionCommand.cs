using AutoMapper;
using HomeBudget.API.CQRS.Events;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Command.Transaction;

public record UpdateTransactionCommand(TransactionDto Transaction) : IRequest<IEvent>;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, IEvent>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IMapper mapper;

    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        this.transactionRepository = transactionRepository;
        this.mapper = mapper;
    }

    public async Task<IEvent> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.GetTransactionByIdAsync(request.Transaction.Id);
        if (transaction is null)
        {
            return new TransactionNotFoundEvent();
        }

        mapper.Map(request.Transaction, transaction);

        await transactionRepository.SaveChangedAsync();

        return new TransactionUpdatedEvent();
    }
}