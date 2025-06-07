using Dedsi.Ddd.CQRS.CommandEventRecorders;
using Dedsi.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;

namespace Dedsi.Ddd.CQRS.EntityFrameworkCore.EntityFrameworkCores;

[ConnectionStringName("Default")]
public class DedsiDddCqrsDbContext(DbContextOptions<DedsiDddCqrsDbContext> options)
    : DedsiEfCoreDbContext<DedsiDddCqrsDbContext>(options)
{
    public DbSet<CommandEventRecorder> CommandEventRecorders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<CommandEventRecorder>(b =>
        {
            b.ToTable("CommandEventRecorders", "Dedsi");
            b.HasKey(x => x.Id);
        });
    }
}