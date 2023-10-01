using AutoMapper;
using HomeBudget.API.CQRS.Events;
using HomeBudget.API.Models.UserModels;
using HomeBudget.API.Services.Repositories;
using HomeBudget.API.Services.Utils;
using MediatR;

namespace HomeBudget.API.CQRS.Command.UserCommand;

public record UpdateUserPasswordCommand(int UserId, UserForUpdatePasswordDto UserPasswordUpdate) : IRequest<IEvent>;

public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, IEvent>
{
    private readonly IUserRepository userRepository;

    public UpdateUserPasswordCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<IEvent> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await userRepository.GetUserByIdAsync(request.UserId);
        if (userEntity is null)
        {
            return new UserNotFoundEvent();
        }

        var salt = Argon2Hasher.GenerateSalt();
        var passwordHash = Argon2Hasher.GenerateHash(request.UserPasswordUpdate.Password, salt);
        userEntity.PasswordSalt = salt;
        userEntity.PasswordHash = passwordHash;
        
        await userRepository.SaveChangesAsync();

        return new UserUpdatedEvent();
    }
}