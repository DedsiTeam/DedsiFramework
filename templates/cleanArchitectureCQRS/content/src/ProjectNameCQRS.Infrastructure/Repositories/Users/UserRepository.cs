using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Repositories.Users;

public interface IUserRepository : IDedsiRepository<User, Guid>
{
    Task<bool> CreateAsync(User user);

    Task<User> GetByIdAsync(Guid id);

    Task UpdateTestAsync(User user);
}

public class TestUserRepository(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider)
    : DedsiEfCoreRepository<ProjectNameCQRSDbContext, User, Guid>(dbContextProvider), 
        IUserRepository
{
    public Task<bool> CreateAsync(User user)
    {
        return Task.FromResult(true);
    }

    public Task<User> GetByIdAsync(Guid id)
    {
        return Task.FromResult(new User(id, "Cohen Wang", "cohen", "123123@qq.com", "123123123@qq.com"));
    }

    public Task UpdateTestAsync(User user)
    {
        return Task.CompletedTask;
    }
}