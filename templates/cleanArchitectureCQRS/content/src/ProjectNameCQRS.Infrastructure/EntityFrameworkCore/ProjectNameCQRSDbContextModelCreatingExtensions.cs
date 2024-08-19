using Microsoft.EntityFrameworkCore;
using ProjectNameCQRS.Roles;
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
            b.ToTable("Users", ProjectNameCQRSDomainOptions.DbTablePrefixByDbo);
            b.HasKey(a => a.Id);
        });

        builder.Entity<Role>(b =>
        {
            b.ToTable("Roles", ProjectNameCQRSDomainOptions.DbTablePrefixByDbo);
            b.HasKey(a => a.Id);
        });

        builder.Entity<UserRole>(b =>
        {
            b.ToTable("UserRoles", ProjectNameCQRSDomainOptions.DbTablePrefixByDbo);
            b.HasKey(a => a.Id);
        });

    }
}