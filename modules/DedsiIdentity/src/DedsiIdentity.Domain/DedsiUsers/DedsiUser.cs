using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace DedsiIdentity.DedsiUsers;

public class DedsiUser : AggregateRoot<string>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; protected set; }

    /// <summary>
    /// 登录账号
    /// </summary>
    public string LoginAccount { get; protected set; }

    /// <summary>
    /// 登录密码(加密)
    /// </summary>
    public string LoginAccountPwd { get; protected set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; protected set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string Phone { get; protected set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    public bool IsDeleted { get; protected set; }


    /// <summary>
    /// 修改用户名
    /// </summary>
    /// <param name="newUserName">新的用户名</param>
    /// <returns>当前用户实例</returns>
    public DedsiUser ChangeUserName(string newUserName)
    {
        UserName = Check.NotNullOrWhiteSpace(newUserName, nameof(UserName));

        return this;
    }

    /// <summary>
    /// 修改登录账号
    /// </summary>
    /// <param name="newLoginAccount">新的登录账号</param>
    /// <returns>当前用户实例</returns>
    public DedsiUser ChangeLoginAccount(string newLoginAccount)
    {
        LoginAccount = Check.NotNullOrWhiteSpace(newLoginAccount, nameof(LoginAccount));

        return this;
    }

    /// <summary>
    /// 修改登录密码
    /// </summary>
    /// <param name="newLoginAccountPwd">新的登录密码</param>
    /// <returns>当前用户实例</returns>
    public DedsiUser ChangeLoginAccountPwd(string newLoginAccountPwd)
    {
        LoginAccountPwd = Check.NotNullOrWhiteSpace(newLoginAccountPwd, nameof(LoginAccountPwd));

        return this;
    }

    /// <summary>
    /// 修改邮箱
    /// </summary>
    /// <param name="newEmail">新的邮箱地址</param>
    /// <returns>当前用户实例</returns>
    public DedsiUser ChangeEmail(string newEmail)
    {
        Email = Check.NotNullOrWhiteSpace(newEmail, nameof(Email));

        return this;
    }

    /// <summary>
    /// 修改电话
    /// </summary>
    /// <param name="newPhone">新的电话号码</param>
    /// <returns>当前用户实例</returns>
    public DedsiUser ChangePhone(string newPhone)
    {
        Phone = Check.NotNullOrWhiteSpace(newPhone, nameof(Phone));

        return this;
    }

    /// <summary>
    /// 用户角色集合
    /// </summary>
    public ICollection<UserRole> Roles { get; protected set; } = new List<UserRole>();

    /// <summary>
    /// 用户权限集合
    /// </summary>
    public ICollection<UserPermission> Permissions { get; protected set; } = new List<UserPermission>();

    /// <summary>
    /// 无参构造函数，供ORM框架使用
    /// </summary>
    protected DedsiUser() { }

    /// <summary>
    /// 创建新用户的构造函数
    /// </summary>
    public DedsiUser(string id, string userName, string loginAccount, string loginPwd): base(id)
    {
        ChangeUserName(userName);
        ChangeLoginAccount(loginAccount);
        ChangeLoginAccountPwd(loginPwd);
        IsDeleted = false;
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
    /// 添加角色
    /// </summary>
    public void AddRole(UserRole role)
    {
        Roles.Add(role);
    }

    /// <summary>
    /// 添加权限
    /// </summary>
    public void AddPermission(UserPermission permission)
    {
        Permissions.Add(permission);
    }
}

/// <summary>
/// 用户的角色类
/// </summary>
public record UserRole(Guid DedsiUserId, Guid RoleId, string Name);

/// <summary>
/// 用户的权限类
/// </summary>
public record UserPermission(Guid DedsiUserId, Guid DedsiPermissionId, string Name, string Code);