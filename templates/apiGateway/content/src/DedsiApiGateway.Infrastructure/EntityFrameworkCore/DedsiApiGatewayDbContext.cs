using Dedsi.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DedsiApiGateway.Users;
using Volo.Abp.Data;

namespace DedsiApiGateway.EntityFrameworkCore;

[ConnectionStringName(DedsiApiGatewayDomainConsts.ConnectionStringName)]
public class DedsiApiGatewayDbContext(DbContextOptions<DedsiApiGatewayDbContext> options) 
    : DedsiEfCoreDbContext<DedsiApiGatewayDbContext>(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureProjectName();
    }

}