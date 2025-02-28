using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using Microsoft.EntityFrameworkCore;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users.Responses;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Users.Queries;

public interface IUserQuery : IDedsiQuery
{
    Task<UserInfoResponse> GetByidAsync(Guid id, CancellationToken cancellationToken = default);
}

public class UserQuery(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider)
    : DedsiEfCoreQuery<ProjectNameCQRSDbContext>(dbContextProvider),
        IUserQuery
{
    public async Task<UserInfoResponse> GetByidAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var userDbSet = await GetDbSetAsync<User>();
        
        var user = await userDbSet.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        return new UserInfoResponse()
        {
            Id = user.Id,
            UserName = user.UserName,
            Account = user.Account,
            Email = user.Email
        };
    }
}