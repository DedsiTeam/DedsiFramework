using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Roles.Commands;

namespace ProjectNameCQRS.Roles;

/// <summary>
/// 角色
/// </summary>
/// <param name="dedsiMediator"></param>
public class RoleController(IDedsiMediator dedsiMediator) : ProjectNameCQRSController
{
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<Guid> CreateRoleAsync(CreateRoleCommand command)
    {
        return dedsiMediator.PublishAsync(command);
    }
}
