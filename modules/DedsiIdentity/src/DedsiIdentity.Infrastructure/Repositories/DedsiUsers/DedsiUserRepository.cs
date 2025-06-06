using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using DedsiIdentity.DedsiUsers;
using DedsiIdentity.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DedsiIdentity.Repositories.DedsiUsers;

/// <summary>
/// DedsiUser Repository
/// </summary>
public interface IDedsiUserRepository: IDedsiCqrsRepository<DedsiUser, string>;

/// <summary>
/// DedsiUser Repository
/// </summary>
/// <param name="dbContextProvider"></param>
public class DedsiUserRepository(IDbContextProvider<DedsiIdentityDbContext> dbContextProvider)
    : DedsiCqrsEfCoreRepository<DedsiIdentityDbContext, DedsiUser, string>(dbContextProvider), IDedsiUserRepository;