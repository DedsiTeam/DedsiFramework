using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.EntityFrameworkCore;

public abstract class DedsiEfCoreDbContext<TDbContext>(DbContextOptions<TDbContext> options) 
    : AbpDbContext<TDbContext>(options), IDedsiEfCoreDbContext
    where TDbContext : DbContext, IDedsiEfCoreDbContext;