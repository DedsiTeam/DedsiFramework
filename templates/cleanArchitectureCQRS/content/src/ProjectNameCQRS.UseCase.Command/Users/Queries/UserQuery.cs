using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Dtos;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Users.Queries;

public interface IUserQuery: IDedsiQuery
{
    Task<SearchUserPagedResultDto> SearchUserAsync(SearchUserCommand command);
}

public class UserQuery(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider) 
    : DedsiEfCoreQuery<ProjectNameCQRSDbContext>(dbContextProvider), 
        IUserQuery
{
    public Task<SearchUserPagedResultDto> SearchUserAsync(SearchUserCommand request)
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