using Volo.Abp.Domain.Entities.Auditing;

namespace ProjectName.Domain.Users;

public class User : CreationAuditedEntity<Guid>
{
    public string UserName { get; set; }
    
    public string Account { get; set; }
    
    public string PassWord { get; set; }
    
    public string Email { get; set; }
}