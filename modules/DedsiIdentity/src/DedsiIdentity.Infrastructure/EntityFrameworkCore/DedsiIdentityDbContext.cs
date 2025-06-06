using Dedsi.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;

namespace DedsiIdentity.EntityFrameworkCore;

[ConnectionStringName(DedsiIdentityDomainConsts.ConnectionStringName)]
public class DedsiIdentityDbContext(DbContextOptions<DedsiIdentityDbContext> options) 
    : DedsiEfCoreDbContext<DedsiIdentityDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureProjectName();
    }

}