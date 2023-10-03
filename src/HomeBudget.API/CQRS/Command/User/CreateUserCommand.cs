using AutoMapper;
using HomeBudget.API.Models.UserModels;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Command.User;

public record CreateUserCommand(UserForCreationDto User, string Password) : IRequest<UserDto>;

public class CreateUserCommandRequestHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public CreateUserCommandRequestHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<Entities.User>(request.User);
        
        userRepository.AddUser(user, request.Password);
        await userRepository.SaveChangesAsync();

        return mapper.Map<UserDto>(user);
    }
}