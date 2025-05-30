using Microsoft.EntityFrameworkCore;
using DedsiApiGateway.Users;
using Volo.Abp;

namespace DedsiApiGateway.EntityFrameworkCore;

public static class DedsiApiGatewayDbContextModelCreatingExtensions
{
    public static void ConfigureProjectName(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<User>(b =>
        {
            b.ToTable("Users", DedsiApiGatewayDomainConsts.DbSchemaName);
            b.HasKey(a => a.Id);
        });
    }
}