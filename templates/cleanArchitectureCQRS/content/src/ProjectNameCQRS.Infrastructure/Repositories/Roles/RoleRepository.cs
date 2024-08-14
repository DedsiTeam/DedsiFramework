using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Roles;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Repositories.Roles;

public interface IRoleRepository : IDedsiRepository<Role, Guid>;

public class RoleRepository(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider)
: DedsiEfCoreRepository<ProjectNameCQRSDbContext, Role, Guid>(dbContextProvider),
    IRoleRepository;
