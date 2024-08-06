using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectName.Infrastructure.EntityFrameworkCore;

public class ProjectNameDbContext(DbContextOptions<ProjectNameDbContext> options)
    : AbpDbContext<ProjectNameDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

}