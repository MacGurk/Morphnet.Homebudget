using AutoMapper;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Command.Transaction;

public record CreateTransactionCommand(TransactionForCreationDto Transaction) : IRequest<TransactionDto?>;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto?>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IUserRepository userRepository, IMapper mapper)
    {
        this.transactionRepository = transactionRepository;
        this.userRepository = userRepository;
        this.mapper = mapper;
    }
    
    public async Task<TransactionDto?> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.Transaction.UserId);
        if (user is null)
        {
            return null;
        }
        var transaction = mapper.Map<Entities.Transaction>(request.Transaction);
        
        transactionRepository.AddTransaction(transaction);
        await transactionRepository.SaveChangedAsync();
        
        return mapper.Map<TransactionDto>(transaction);
    }
}