using Dedsi.Ddd.Domain.Shared.EntityIds;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore;

public abstract class DedsiEfCoreDbContext<TDbContext>(DbContextOptions<TDbContext> options)
    : AbpDbContext<TDbContext>(options), IDedsiEfCoreDbContext
    where TDbContext : DbContext, IDedsiEfCoreDbContext
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Int64StronglyTypedId>()
            .HaveConversion<Int64StronglyTypedIdConverter>();
        
        configurationBuilder
            .Properties<GuidStronglyTypedId>()
            .HaveConversion<GuidStronglyTypedIdConverter>();
        
        configurationBuilder
            .Properties<UlidStronglyTypedId>()
            .HaveConversion<UlidStronglyTypedIdConverter>();
    }
}

