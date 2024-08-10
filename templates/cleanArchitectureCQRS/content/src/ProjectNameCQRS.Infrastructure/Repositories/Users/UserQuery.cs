using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Repositories.Users;

public interface IUserQuery: IDedsiQuery
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User> GetByIdAsync(Guid id);
}

public class UserQuery(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider) 
    : DedsiDapperQuery<ProjectNameCQRSDbContext>(dbContextProvider), 
        IUserQuery
{
    public Task<User> GetByIdAsync(Guid id)
    {
        return Task.FromResult(new User(id,"Cohen Wang", "cohen", "123123@qq.com","123123123@qq.com"));
    }
}