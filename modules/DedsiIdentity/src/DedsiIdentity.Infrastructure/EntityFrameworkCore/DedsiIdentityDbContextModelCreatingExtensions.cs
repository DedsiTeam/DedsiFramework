using DedsiIdentity.DedsiRoles;
using DedsiIdentity.DedsiUsers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace DedsiIdentity.EntityFrameworkCore;

public static class DedsiIdentityDbContextModelCreatingExtensions
{
    public static void ConfigureProjectName(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        #region DedsiUsers
        builder.Entity<DedsiUser>(b =>
        {
            b.ToTable("DedsiUsers", DedsiIdentityDomainConsts.DbSchemaName);
            b.HasKey(x => x.Id);

            b
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.DedsiUserId)
                .IsRequired();

            b
                .HasMany(e => e.Permissions)
                .WithOne()
                .HasForeignKey(e => e.DedsiUserId)
                .IsRequired();
        });

        builder.Entity<UserRole>(b =>
        {
            b.ToTable("UserRoles", DedsiIdentityDomainConsts.DbSchemaName);
            b.HasKey(x => new { x.DedsiUserId, x.RoleId });
        });

        builder.Entity<UserPermission>(b =>
        {
            b.ToTable("UserPermissions", DedsiIdentityDomainConsts.DbSchemaName);
            b.HasKey(x => new { x.DedsiUserId, x.PermissionId });
        });
        #endregion

        #region DedsiRole
        builder.Entity<DedsiRole>(b =>
        {
            b.ToTable("DedsiRoles", DedsiIdentityDomainConsts.DbSchemaName);
            b.HasKey(x => x.Id);


            b
                .HasMany(e => e.ChildRoles)
                .WithOne()
                .HasForeignKey(e => e.DedsiRoleId)
                .IsRequired();


            b
                .HasMany(e => e.ExclusiveRoles)
                .WithOne()
                .HasForeignKey(e => e.DedsiRoleId)
                .IsRequired();
        });

        builder.Entity<ChildRole>(b =>
        {
            b.ToTable("ChildRoles", DedsiIdentityDomainConsts.DbSchemaName);
            b.HasKey(x => new { x.DedsiRoleId, x.RoleId });
        });

        builder.Entity<MutuallyExclusiveRole>(b =>
        {
            b.ToTable("MutuallyExclusiveRoles", DedsiIdentityDomainConsts.DbSchemaName);
            b.HasKey(x => new { x.DedsiRoleId, x.RoleId });
        });
        #endregion
    }
}