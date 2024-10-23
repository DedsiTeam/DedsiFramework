using Volo.Abp.Domain.Entities;

namespace ProjectName.Users;

public class User : AggregateRoot<Guid>
{
    public string UserName { get; set; }
    
    public string Account { get; set; }
    
    public string PassWord { get; set; }
    
    public string Email { get; set; }
}