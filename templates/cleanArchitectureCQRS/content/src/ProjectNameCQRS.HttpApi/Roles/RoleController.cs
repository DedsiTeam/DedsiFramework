using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Roles.Commands;
using ProjectNameCQRS.Roles.Dtos;

namespace ProjectNameCQRS.Roles;

/// <summary>
/// 角色
/// </summary>
/// <param name="dedsiMediator"></param>
public class RoleController(IDedsiMediator dedsiMediator) : ProjectNameCQRSController
{
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<Guid> CreateAsync(CreateRoleInputDto input)
    {
        return dedsiMediator.PublishAsync(new CreateRoleCommand(input.RoleCode, input.RoleName));
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<bool> UpdateAsync(UpdateRoleInputDto input)
    {
        return dedsiMediator.PublishAsync(new UpdateRoleCommand(input.Id, input.RoleCode, input.RoleName));
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public Task<bool> DeleteAsync(Guid id)
    {
        return dedsiMediator.PublishAsync(new DeleteRoleCommand(id));
    }
}
