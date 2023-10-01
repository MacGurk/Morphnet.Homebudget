using AutoMapper;
using HomeBudget.API.CQRS.Events;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Command.UserCommand;

public record DeleteUserCommand(int UserId) : IRequest<IEvent>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IEvent>
{
    private readonly IUserRepository userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<IEvent> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await userRepository.GetUserByIdAsync(request.UserId);
        if (userEntity is null)
        {
            return new UserNotFoundEvent();
        }
        
        userRepository.DeleteUser(userEntity);
        await userRepository.SaveChangesAsync();

        return new UserDeletedEvent();
    }
}
