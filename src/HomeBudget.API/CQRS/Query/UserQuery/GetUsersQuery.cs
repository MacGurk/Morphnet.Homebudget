using AutoMapper;
using HomeBudget.API.Models.UserModels;
using HomeBudget.API.Services.Repositories;
using MediatR;

namespace HomeBudget.API.CQRS.Query.UserQuery;

public record GetUsersQuery(bool IsContributor = false) : IRequest<IEnumerable<UserDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<UserDto>>(await userRepository.GetUsersAsync(request.IsContributor));
    }
}