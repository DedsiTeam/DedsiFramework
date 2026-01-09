using Dedsi.Ddd.Domain.Repositories;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Repositories;

public class DedsiDddEfCoreRepository<TDbContext, TDomain, TKey>(IDbContextProvider<TDbContext> dbContextProvider)
    : EfCoreRepository<TDbContext, TDomain, TKey>(dbContextProvider), IDedsiCqrsRepository<TDomain, TKey>
    where TDbContext : IDedsiEfCoreDbContext
    where TDomain : class, IAggregateRoot<TKey>;