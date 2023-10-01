using AutoMapper;
using HomeBudget.API.CQRS.Events;
using HomeBudget.API.Models.UserModels;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Command.UserCommand;

public record UpdateUserCommand(UserDto User) : IRequest<IEvent>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IEvent>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<IEvent> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.User.Id);
        if (user is null)
        {
            return new UserNotFoundEvent();
        }

        mapper.Map(request.User, user);

        await userRepository.SaveChangesAsync();

        return new UserUpdatedEvent();
    }
}