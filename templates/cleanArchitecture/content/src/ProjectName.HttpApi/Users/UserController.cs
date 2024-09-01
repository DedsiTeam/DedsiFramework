using Microsoft.AspNetCore.Mvc;

namespace ProjectName.Users;

public class UserController(IUserAppService userAppService) : ProjectNameController
{
    /// <summary>
    /// 示例查询
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<User> GetAsync()
    {
        return userAppService.GetAsync();
    }
}