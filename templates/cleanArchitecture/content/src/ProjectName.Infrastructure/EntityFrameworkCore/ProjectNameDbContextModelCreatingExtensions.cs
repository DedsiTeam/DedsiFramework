using Microsoft.EntityFrameworkCore;
using ProjectName.Users;
using Volo.Abp;

namespace ProjectName.EntityFrameworkCore;

public static class ProjectNameDbContextModelCreatingExtensions
{
    public static void ConfigureProjectName(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
        
        builder.Entity<User>(b =>
        {
            b.ToTable("Users", ProjectNameDomainOptions.DbTablePrefix);
            b.HasKey(a => a.Id);
        });

    }
}