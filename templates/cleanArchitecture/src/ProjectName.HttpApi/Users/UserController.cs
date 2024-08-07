using Microsoft.AspNetCore.Mvc;

namespace ProjectName.Users;

public class UserController(IUserAppService userAppService) : ProjectNameController
{
    [HttpGet]
    public Task<User> GetAsync()
    {
        return userAppService.GetAsync();
    }
}