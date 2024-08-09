using Volo.Abp.Domain.Entities;

namespace ProjectNameCQRS.Users;

public class User : Entity<Guid>
{
    public User(){}
    public User(Guid id): base(id){}
    
    public string UserName { get; set; }
    
    public string Account { get; set; }
    
    public string PassWord { get; set; }
    
    public string Email { get; set; }
}