using Volo.Abp.Domain.Entities;

namespace ProjectNameCQRS.Users;

public class UserRole : Entity<Guid>
{
    public UserRole() { }

    public UserRole(Guid id, Guid userId, Guid roleId, string roleName) : base(id)
    {
        UserId = userId;
        RoleId = roleId;
        RoleName = roleName;
    }

    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public string RoleName { get; set; }
}
