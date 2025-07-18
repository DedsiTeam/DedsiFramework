using Dedsi.EntityFrameworkCore;
using DedsiIdentity.DedsiRoles;
using DedsiIdentity.DedsiUsers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;

namespace DedsiIdentity.EntityFrameworkCore;

[ConnectionStringName(DedsiIdentityDomainConsts.ConnectionStringName)]
public class DedsiIdentityDbContext(DbContextOptions<DedsiIdentityDbContext> options) 
    : DedsiEfCoreDbContext<DedsiIdentityDbContext>(options)
{

    public DbSet<DedsiUser> DedsiUsers { get; set; }
    
    public DbSet<DedsiRole> DedsiRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureProjectName();
    }

}