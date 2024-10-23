using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using Mapster;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Dtos;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Users.Queries;

public interface IUserQuery : IDedsiEfCoreQuery
{
    Task<UserDto> GetByidAsync(Guid id, CancellationToken cancellationToken = default);
}

public class UserQuery(
    IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider,
    IUserRepository userRepository)
    : DedsiEfCoreQuery<ProjectNameCQRSDbContext>(dbContextProvider),
        IUserQuery
{
    public async Task<UserDto> GetByidAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(id, true, cancellationToken);

        return user.Adapt<UserDto>();
    }
}