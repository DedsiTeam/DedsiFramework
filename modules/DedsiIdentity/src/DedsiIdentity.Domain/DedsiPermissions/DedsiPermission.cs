using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace DedsiIdentity.DedsiPermissions;

public class DedsiPermission : AggregateRoot<string>
{
    /// <summary>
    /// 权限名称
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// 权限说明
    /// </summary>
    public string Description { get; protected set; }

    /// <summary>
    /// 权限组名称
    /// </summary>
    public string GroupName { get; protected set; }

    /// <summary>
    /// 权限组编码
    /// </summary>
    public string GroupCode { get; protected set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    public bool IsDeleted { get; protected set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool IsVisible { get; protected set; }

    /// <summary>
    /// 无参构造函数，供ORM框架使用
    /// </summary>
    protected DedsiPermission() { }

    /// <summary>
    /// 创建新权限的构造函数
    /// </summary>
    public DedsiPermission(string id,string name, string description, string groupName, string groupCode): base(id)
    {
        ChangeName(name);
        ChangeDescription(description);
        ChangeGroupName(groupName);
        ChangeGroupCode(groupCode);
        IsDeleted = false;
        IsVisible = true;
    }

    /// <summary>
    /// 修改权限名称
    /// </summary>
    /// <param name="newName">新的权限名称</param>
    /// <returns>当前权限实例</returns>
    public DedsiPermission ChangeName(string newName)
    {
        Name = Check.NotNullOrWhiteSpace(newName, nameof(Name));
        return this;
    }

    /// <summary>
    /// 修改权限说明
    /// </summary>
    /// <param name="newDescription">新的权限说明</param>
    /// <returns>当前权限实例</returns>
    public DedsiPermission ChangeDescription(string newDescription)
    {
        Description = newDescription;
        return this;
    }

    /// <summary>
    /// 修改权限组名称
    /// </summary>
    /// <param name="newGroupName">新的权限组名称</param>
    /// <returns>当前权限实例</returns>
    public DedsiPermission ChangeGroupName(string newGroupName)
    {
        GroupName = Check.NotNullOrWhiteSpace(newGroupName, nameof(GroupName));
        return this;
    }

    /// <summary>
    /// 修改权限组编码
    /// </summary>
    /// <param name="newGroupCode">新的权限组编码</param>
    /// <returns>当前权限实例</returns>
    public DedsiPermission ChangeGroupCode(string newGroupCode)
    {
        GroupCode = Check.NotNullOrWhiteSpace(newGroupCode, nameof(GroupCode));
        return this;
    }

    /// <summary>
    /// 设置显示状态
    /// </summary>
    /// <param name="isVisible">是否显示</param>
    /// <returns>当前权限实例</returns>
    public DedsiPermission SetVisibility(bool isVisible)
    {
        IsVisible = isVisible;
        return this;
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
}