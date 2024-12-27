using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Dtos;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Users.Queries;

public interface IUserQuery : IDedsiQuery
{
    Task<UserDto> GetByidAsync(Guid id, CancellationToken cancellationToken = default);
}

public class UserQuery(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider)
    : DedsiEfCoreQuery<ProjectNameCQRSDbContext>(dbContextProvider),
        IUserQuery
{
    public async Task<UserDto> GetByidAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync<User>();
        
        var user = await dbSet.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        return user.Adapt<UserDto>();
    }
}