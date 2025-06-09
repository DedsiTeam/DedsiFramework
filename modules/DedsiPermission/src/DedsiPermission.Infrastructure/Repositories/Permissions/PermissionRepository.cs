using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using DedsiPermission.EntityFrameworkCore;
using DedsiPermission.Permissions;
using Volo.Abp.EntityFrameworkCore;

namespace DedsiPermission.Repositories.Permissions;
/// <summary>
/// Permission Repository
/// </summary>
public interface IPermissionRepository : IDedsiCqrsRepository<Permission, string>;

/// <summary>
/// Permission Repository
/// </summary>
/// <param name="dbContextProvider"></param>
public class PermissionRepository(IDbContextProvider<DedsiPermissionDbContext> dbContextProvider)
    : DedsiCqrsEfCoreRepository<DedsiPermissionDbContext, Permission, string>(dbContextProvider), IPermissionRepository;
