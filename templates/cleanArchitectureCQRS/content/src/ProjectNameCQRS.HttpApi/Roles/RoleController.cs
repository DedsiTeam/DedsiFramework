using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS;
using ProjectNameCQRS.Roles.Commands;
using Volo.Abp.EventBus.Local;

namespace ProjectNameCQRS.Roles;

public class RoleController(ILocalEventBus localEventBus) : ProjectNameCQRSController
{
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> CreateRoleAsync(CreateRoleCommand command)
    {
        await localEventBus.PublishAsync(command);

        return true;
    }
}
