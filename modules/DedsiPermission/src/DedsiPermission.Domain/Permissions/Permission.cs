using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace DedsiPermission.Permissions;

/// <summary>
/// 权限
/// </summary>
public class Permission : AggregateRoot<string>
{
    protected Permission()
    {
    }

    /// <summary>
    /// 创建权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <param name="permissionName">权限名称</param>
    /// <param name="permissionCode">权限标识</param>
    /// <param name="permissionDescription">权限描述</param>
    /// <param name="permissionType">权限类型</param>
    /// <param name="isEnabled">是否启用</param>
    /// <param name="parentId">父ID</param>
    public Permission(
        string id,
        string permissionName,
        string permissionCode,
        string permissionDescription,
        PermissionType permissionType,
        string permissionGroupCode,
        string permissionGroupName,
        bool isEnabled,
        string? parentId = null) : base(id)
    {
        ChangeParentId(parentId);
        ChangePermissionName(permissionName);
        ChangePermissionCode(permissionCode);
        ChangePermissionDescription(permissionDescription);
        ChangePermissionType(permissionType);
        ChangePermissionGroupCode(permissionGroupCode);
        ChangePermissionGroupName(permissionGroupName);
        EnabledPermission();
    }

    /// <summary>
    /// 父Id
    /// </summary>
    public string? ParentId { get; protected set; }

    /// <summary>
    /// 更改父Id
    /// </summary>
    public void ChangeParentId(string? parentId)
    {
        ParentId = parentId;
    }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; protected set; }

    /// <summary>
    /// 更改权限名称
    /// </summary>
    public void ChangePermissionName(string permissionName)
    {
        PermissionName = Check.NotNullOrWhiteSpace(permissionName, nameof(PermissionName));
    }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string PermissionCode { get; protected set; }

    /// <summary>
    /// 更改权限标识
    /// </summary>
    public void ChangePermissionCode(string permissionCode)
    {
        PermissionCode = Check.NotNullOrWhiteSpace(permissionCode, nameof(PermissionCode));
    }

    /// <summary>
    /// 描述
    /// </summary>
    public string PermissionDescription { get; protected set; }

    /// <summary>
    /// 更改权限描述
    /// </summary>
    public void ChangePermissionDescription(string permissionDescription)
    {
        PermissionDescription = Check.NotNullOrWhiteSpace(permissionDescription, nameof(PermissionDescription));
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public PermissionType PermissionType { get; protected set; }

    /// <summary>
    /// 更改权限类型
    /// </summary>
    public void ChangePermissionType(PermissionType permissionType)
    {
        PermissionType = permissionType;
    }

    /// <summary>
    /// 权限组Code
    /// </summary>
    public string PermissionGroupCode { get; protected set; }
   
    public void ChangePermissionGroupCode(string permissionGroupCode)
    {
        PermissionGroupCode = Check.NotNullOrWhiteSpace(permissionGroupCode, nameof(PermissionGroupCode));
    }

    /// <summary>
    /// 权限组名称
    /// </summary>
    public string PermissionGroupName { get; protected set; }

    public void ChangePermissionGroupName(string permissionGroupName)
    {
        PermissionGroupName = Check.NotNullOrWhiteSpace(permissionGroupName, nameof(PermissionGroupName));
    }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; protected set; }

    public void EnabledPermission()
    {
        IsEnabled = true;
    }

    /// <summary>
    /// 禁用权限
    /// </summary>
    public void DisablePermission()
    {
        IsEnabled = false;
    }

    /// <summary>
    /// 子权限
    /// </summary>
    public ICollection<Permission> ChildPermissions { get; protected set; } = new List<Permission>();

    public void AddChildPermission(Permission childPermission)
    {
        childPermission.ParentId = Id;
        ChildPermissions.Add(childPermission);
    }

    public void RemoveChildPermission(Permission childPermission)
    {
        if (ChildPermissions.Contains(childPermission))
        {
            ChildPermissions.Remove(childPermission);
        }
    }
}

/// <summary>
/// 权限类型
/// </summary>  
public enum PermissionType
{
    /// <summary>
    /// 应用
    /// </summary>
    Application = 1,

    /// <summary>
    /// 功能点
    /// </summary>
    FunctionPoint = 2
}