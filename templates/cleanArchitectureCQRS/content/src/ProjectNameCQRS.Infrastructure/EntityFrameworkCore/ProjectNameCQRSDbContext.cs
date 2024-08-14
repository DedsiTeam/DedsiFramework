using Dedsi.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectNameCQRS.Roles;
using ProjectNameCQRS.Users;
using Volo.Abp.Data;

namespace ProjectNameCQRS.EntityFrameworkCore;

[ConnectionStringName(ProjectNameCQRSDomainOptions.ConnectionStringName)]
public class ProjectNameCQRSDbContext(DbContextOptions<ProjectNameCQRSDbContext> options) 
    : DedsiEfCoreDbContext<ProjectNameCQRSDbContext>(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureProjectName();
    }

}