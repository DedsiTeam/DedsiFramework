using Microsoft.EntityFrameworkCore;
using ProjectNameCQRS.Users;
using Volo.Abp;

namespace ProjectNameCQRS.EntityFrameworkCore;

public static class ProjectNameCQRSDbContextModelCreatingExtensions
{
    public static void ConfigureProjectName(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
        
        builder.Entity<User>(b =>
        {
            b.ToTable("Users", ProjectNameCQRSDomainOptions.DbTablePrefix);
            b.HasKey(a => a.Id);
        });

    }
}