using Dedsi.Ddd.Domain.Queries;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Queries
{
    public class DedsiDapperQuery<TDbContext>(IDbContextProvider<TDbContext> dbContextProvider)
    : DapperRepository<TDbContext>(dbContextProvider), IDedsiQuery
        where TDbContext : IDedsiEfCoreDbContext
    {

    }
}
