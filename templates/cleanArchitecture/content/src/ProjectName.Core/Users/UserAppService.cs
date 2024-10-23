using Dedsi.Ddd.Application.Contracts.Services;
using Dedsi.Ddd.Application.Services;

namespace ProjectName.Users;

public interface IUserAppService : IDedsiApplicationService
{
    Task<User> GetAsync();
}

public class UserAppService(IUserRepository userRepository) : DedsiApplicationService, IUserAppService
{
    public Task<User> GetAsync()
    {
        return userRepository.GetAsync(GuidGenerator.Create());
    }
}