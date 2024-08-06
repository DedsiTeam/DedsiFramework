using Dedsi.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectName.Infrastructure.EntityFrameworkCore;

public class ProjectNameDbContext(DbContextOptions<ProjectNameDbContext> options)
    : DedsiEfCoreDbContext<ProjectNameDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

}