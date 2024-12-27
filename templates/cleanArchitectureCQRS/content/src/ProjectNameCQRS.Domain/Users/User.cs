using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace ProjectNameCQRS.Users;

public class User : AggregateRoot<Guid>
{
    protected User() { }

    public User(Guid id, string userName, string account, string passWord, string email) : base(id)
    {
        UserName = userName;
        Account = account;
        PassWord = passWord;
        Email = email;
    }

    public string UserName { get; private set; }

    public string Account { get; private set; }

    public string PassWord { get; private set; }

    public string Email { get; private set; }

    public void Update(string userName, string account, string email)
    {
        UserName = Check.NotNullOrWhiteSpace(userName, nameof(userName));
        Account = Check.NotNullOrWhiteSpace(account, nameof(account));
        Email = Check.NotNullOrWhiteSpace(email, nameof(email));
    }

}