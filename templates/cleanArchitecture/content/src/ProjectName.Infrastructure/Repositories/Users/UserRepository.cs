using Dedsi.EntityFrameworkCore.Repositories;
using ProjectName.EntityFrameworkCore;
using ProjectName.Users;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectName.Repositories.Users;

public class UserRepository(IDbContextProvider<ProjectNameDbContext> dbContextProvider)
    : DedsiEfCoreRepository<ProjectNameDbContext, User, Guid>(dbContextProvider), 
        IUserRepository
{
    public Task<User> GetByIdAsync(Guid id)
    {
        return Task.FromResult(new User()
        {
            UserName = "ProjectName",
            Account = "ProjectName",
            PassWord = "ProjectName@" + DateTime.Now,
            Email = "123465789@qq.com"
        });
    }
}