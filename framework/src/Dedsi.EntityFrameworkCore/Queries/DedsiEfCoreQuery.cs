using Dedsi.Ddd.Domain.Queries;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Queries;

public class DedsiEfCoreQuery<TDbContext>(IDbContextProvider<TDbContext> dbContextProvider) : IDedsiQuery
    where TDbContext : IDedsiEfCoreDbContext
{
    protected virtual Task<TDbContext> GetDbContextAsync() => dbContextProvider.GetDbContextAsync();
}