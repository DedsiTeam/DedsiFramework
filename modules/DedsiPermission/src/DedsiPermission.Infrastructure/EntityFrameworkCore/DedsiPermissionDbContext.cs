using Dedsi.EntityFrameworkCore;
using DedsiPermission.Permissions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;

namespace DedsiPermission.EntityFrameworkCore;

[ConnectionStringName(DedsiPermissionDomainConsts.ConnectionStringName)]
public class DedsiPermissionDbContext(DbContextOptions<DedsiPermissionDbContext> options) 
    : DedsiEfCoreDbContext<DedsiPermissionDbContext>(options)
{
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureProjectName();
    }
}