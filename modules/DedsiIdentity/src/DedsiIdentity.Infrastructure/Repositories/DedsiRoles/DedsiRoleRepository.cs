using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using DedsiIdentity.DedsiRoles;
using DedsiIdentity.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DedsiIdentity.Repositories.DedsiRoles;

/// <summary>
/// DedsiRole Repository
/// </summary>
public interface IDedsiRoleRepository : IDedsiCqrsRepository<DedsiRole, string>;

/// <summary>
/// DedsiRole Repository
/// </summary>
/// <param name="dbContextProvider"></param>
public class DedsiRoleRepository(IDbContextProvider<DedsiIdentityDbContext> dbContextProvider)
    : DedsiCqrsEfCoreRepository<DedsiIdentityDbContext, DedsiRole, string>(dbContextProvider), IDedsiRoleRepository;