using Dedsi.Ddd.Domain.Entities.Auditing;
using SqlSugar;

namespace ProjectName.Domain.Users;

[SugarTable("dbo.Users")]
public class User : CreationAuditedEntity<Guid>
{
    public string UserName { get; set; }
    
    public string Account { get; set; }
    
    public string PassWord { get; set; }
    
    public string Email { get; set; }
}