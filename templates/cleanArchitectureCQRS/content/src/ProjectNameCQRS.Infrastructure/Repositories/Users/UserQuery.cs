using ProjectNameCQRS.Users;
using Volo.Abp.DependencyInjection;

namespace ProjectNameCQRS.Repositories.Users;

public interface IUserQuery: ITransientDependency
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User> GetByIdAsync(Guid id);
}

public class UserQuery : IUserQuery
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