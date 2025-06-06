using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using DedsiIdentity.DedsiPermissions;
using DedsiIdentity.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DedsiIdentity.Repositories.DedsiPermissions;

/// <summary>
/// DedsiPermission Repository
/// </summary>
public interface IDedsiPermissionRepository : IDedsiCqrsRepository<DedsiPermission, string>;

/// <summary>
/// DedsiPermission Repository
/// </summary>
/// <param name="dbContextProvider"></param>
public class DedsiPermissionRepository(IDbContextProvider<DedsiIdentityDbContext> dbContextProvider)
    : DedsiCqrsEfCoreRepository<DedsiIdentityDbContext, DedsiPermission, string>(dbContextProvider), IDedsiPermissionRepository;