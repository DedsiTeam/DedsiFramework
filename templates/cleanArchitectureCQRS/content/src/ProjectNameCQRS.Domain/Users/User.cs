using ProjectNameCQRS.Roles;
using ProjectNameCQRS.Users.DomainEvents;
using Volo.Abp.Domain.Entities;

namespace ProjectNameCQRS.Users;

public class User : AggregateRoot<Guid>
{
    public User() { }

    public User(Guid id, string userName, string account, string passWord, string email) : base(id)
    {
        UserName = userName;
        Account = account;
        PassWord = passWord;
        Email = email;
        UserRoles = new List<UserRole>();
    }

    public User(Guid id, string userName, string account, string passWord, string email, Role role) : base(id)
    {
        UserName = userName;
        Account = account;
        PassWord = passWord;
        Email = email;
        UserRoles = new List<UserRole>()
        {
            new (Guid.NewGuid(),id, role.Id,role.RoleName)
        };
        AddLocalEvent(new CreateUserSendEmailEvent(this));
    }

    public string UserName { get; private set; }

    public string Account { get; private set; }

    public string PassWord { get; private set; }

    public string Email { get; private set; }

    public ICollection<UserRole> UserRoles { get; private set; }

    public void SetUserRole(UserRole userRole)
    {
        if (UserRoles == null)
        {
            UserRoles = new List<UserRole>();
        }
        UserRoles.Add(userRole);
    }

}