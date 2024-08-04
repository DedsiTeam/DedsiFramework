using Dedsi.Ddd.Application.Contracts.Services;
using Dedsi.Ddd.Application.Services;

namespace ProjectName.Domain.Users;

public interface IUserAppService : IDedsiApplicationService
{
    Task<string> GetAsync();
}

public class UserAppService : DedsiApplicationService, IUserAppService
{
    public Task<string> GetAsync()
    {
        return Task.FromResult("123");
    }
}

public interface IUserAAppService : IDedsiApplicationService
{
    Task<string> GetAsync();
}

public class UserAAppService : DedsiApplicationService, IUserAAppService
{
    public Task<string> GetAsync()
    {
        return Task.FromResult("123");
    }
}