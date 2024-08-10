using Dedsi.Ddd.CQRS;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Dtos;

namespace ProjectNameCQRS.Users.CommandHandlers;

public class SearchUserCommandHandler(IUserQuery userQuery) : IDedsiCommandHandler<SearchUserCommand,SearchUserPagedResultDto>
{
    public Task<SearchUserPagedResultDto> Handle(SearchUserCommand request, CancellationToken cancellationToken)
    {
        var result = new SearchUserPagedResultDto()
        {
            TotalCount = 10,
            Items = new List<UserDto>()
            {
                new (),
                new (),
                new (),
            }
        };
        return Task.FromResult(result);
    }
}