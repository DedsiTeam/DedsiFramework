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
        return userRepository.GetAsync(a => a.Id == Guid.Parse("E5F3305F-567D-45FB-15E4-3A13BF036337"));
    }
}