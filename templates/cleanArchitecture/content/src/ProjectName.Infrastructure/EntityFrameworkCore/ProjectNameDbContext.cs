using Dedsi.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectName.Users;
using Volo.Abp.Data;

namespace ProjectName.EntityFrameworkCore;

[ConnectionStringName(ProjectNameDomainOptions.ConnectionStringName)]
public class ProjectNameDbContext(DbContextOptions<ProjectNameDbContext> options) : DedsiEfCoreDbContext<ProjectNameDbContext>(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureProjectName();
    }

}