using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using Microsoft.EntityFrameworkCore;
using ProjectNameCQRS.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Users.Queries;

public interface IUserQuery : IDedsiQuery
{
    Task<User?> GetByIdAsync(Guid id);
}

public class UserQuery(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider)
    : DedsiEfCoreQuery<ProjectNameCQRSDbContext>(dbContextProvider),
        IUserQuery
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Users.Include(b => b.UserRoles).SingleOrDefaultAsync(a => a.Id == id);
    }
}