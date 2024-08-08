using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Repositories.Users;

public interface IUserQuery: IDedsiQuery<User,Guid>
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User> GetByIdAsync(Guid id);
}

public class UserQuery(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider) 
    : DedsiEfCoreQuery<ProjectNameCQRSDbContext,User,Guid>(dbContextProvider), 
        IUserQuery
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