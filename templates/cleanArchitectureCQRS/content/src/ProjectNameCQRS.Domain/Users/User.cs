using Volo.Abp.Domain.Entities;

namespace ProjectNameCQRS.Users;

public class User : AggregateRoot<Guid>
{
    public User(){ }

    public User(Guid id,string userName,string account,string passWord, string email) : base(id)
    {
        this.UserName = userName;
        this.Account = account;
        this.PassWord = passWord;
        this.Email = email;
    }
    
    public string UserName { get; private set; }
    
    public string Account { get; private set; }
    
    public string PassWord { get; private set; }
    
    public string Email { get; private set; }
}