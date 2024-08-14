using Volo.Abp.Domain.Entities;

namespace ProjectNameCQRS.Roles;

public class Role : AggregateRoot<Guid>
{
    public Role() { }

    public Role(Guid id, string roleCode, string roleName) : base(id)
    {
        RoleCode = roleCode;
        RoleName = roleName;
    }

    /// <summary>
    /// 
    /// </summary>
    public string RoleCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string RoleName { get; set; }
}
