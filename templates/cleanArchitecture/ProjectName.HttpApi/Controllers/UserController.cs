using Microsoft.AspNetCore.Mvc;
using ProjectName.Domain.Users;

namespace ProjectName.HttpApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController(IUserAppService userAppService) : ControllerBase
{
    [HttpGet]
    public Task<User> GetAsync() => userAppService.GetAsync();
}