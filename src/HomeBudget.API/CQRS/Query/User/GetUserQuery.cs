using AutoMapper;
using HomeBudget.API.Models.UserModels;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Query.User;

public record GetUserQuery(int UserId) : IRequest<UserDto?>;

public class GetUserQueryRequestHandler : IRequestHandler<GetUserQuery, UserDto?>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public GetUserQueryRequestHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<UserDto>(await userRepository.GetUserByIdAsync(request.UserId));
    }
}