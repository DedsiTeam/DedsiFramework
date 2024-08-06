using Dedsi.Ddd.Application.Contracts.Services;
using Dedsi.Ddd.Application.Services;

namespace ProjectName.Domain.Users;

public interface IUserAppService : IDedsiApplicationService
{
    Task<User> GetAsync();
}

public class UserAppService(IUserDedsiRepository userDedsiRepository) : DedsiApplicationService, IUserAppService
{
    public Task<User> GetAsync()
    {
        return userDedsiRepository.GetAsync(a => a.Id == Guid.Parse("E5F3305F-567D-45FB-15E4-3A13BF036337"));
    }
}