using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace DedsiIdentity.DedsiRoles;

/// <summary>
/// 角色
/// </summary>
public class DedsiRole : AggregateRoot<string>
{
    /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; protected set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; protected set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    public bool IsDeleted { get; protected set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool IsVisible { get; protected set; }

    /// <summary>
    /// 修改角色编码
    /// </summary>
    /// <param name="newRoleCode">新的角色编码</param>
    /// <returns>当前角色实例</returns>
    public DedsiRole ChangeRoleCode(string newRoleCode)
    {
        RoleCode = Check.NotNullOrWhiteSpace(newRoleCode, nameof(RoleCode));
        return this;
    }

    /// <summary>
    /// 修改角色名称
    /// </summary>
    /// <param name="newRoleName">新的角色名称</param>
    /// <returns>当前角色实例</returns>
    public DedsiRole ChangeRoleName(string newRoleName)
    {
        RoleName = Check.NotNullOrWhiteSpace(newRoleName, nameof(RoleName));
        return this;
    }

    /// <summary>
    /// 角色权限集合
    /// </summary>
    public ICollection<RolePermission> Permissions { get; protected set; } = new List<RolePermission>();

    /// <summary>
    /// 子角色集合
    /// </summary>
    public ICollection<ChildRole> ChildRoles { get; protected set; } = new List<ChildRole>();

    /// <summary>
    /// 互斥角色集合
    /// </summary>
    public ICollection<MutuallyExclusiveRole> ExclusiveRoles { get; protected set; } = new List<MutuallyExclusiveRole>();

    /// <summary>
    /// 无参构造函数，供ORM框架使用
    /// </summary>
    protected DedsiRole() { }

    /// <summary>
    /// 创建新角色的构造函数
    /// </summary>
    public DedsiRole(string id, string roleCode, string roleName): base(id)
    {
        ChangeRoleCode(roleCode);
        ChangeRoleName(roleName);
        IsDeleted = false;
        IsVisible = true;
    }

    /// <summary>
    /// 软删除
    /// </summary>
    public void Delete()
    {
        IsDeleted = true;
    }

    /// <summary>
    /// 恢复删除
    /// </summary>
    public void RestoreDelete()
    {
        IsDeleted = false;
    }

    /// <summary>
    /// 设置显示状态
    /// </summary>
    public void SetVisibility(bool isVisible)
    {
        IsVisible = isVisible;
    }

    /// <summary>
    /// 添加权限
    /// </summary>
    public void AddPermission(RolePermission permission)
    {
        Permissions.Add(permission);
    }

    /// <summary>
    /// 添加子角色
    /// </summary>
    public void AddChildRole(ChildRole childRole)
    {
        ChildRoles.Add(childRole);
    }

    /// <summary>
    /// 添加互斥角色
    /// </summary>
    public void AddExclusiveRole(MutuallyExclusiveRole exclusiveRole)
    {
        ExclusiveRoles.Add(exclusiveRole);
    }
}

/// <summary>
/// 角色权限类
/// </summary>
public class RolePermission
{
    public Guid DedsiRoleId { get; protected set; }

    public Guid DedsiPermissionId { get; protected set; }

    public string Name { get; protected set; }
    public string Code { get; protected set; }

    protected RolePermission() { }

    public RolePermission(Guid roleId, Guid permissionId, string name, string code)
    {
        DedsiRoleId = roleId;
        DedsiPermissionId = permissionId;
        Name = name;
        Code = code;
    }
}

/// <summary>
/// 子角色类
/// </summary>
public record ChildRole(Guid DedsiRoleId, Guid RoleId, string Name);


/// <summary>
/// 互斥角色类
/// </summary>
public record MutuallyExclusiveRole(Guid DedsiRoleId, Guid RoleId, string Name);