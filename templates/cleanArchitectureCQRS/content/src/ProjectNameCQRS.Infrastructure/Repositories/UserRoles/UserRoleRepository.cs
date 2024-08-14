using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Repositories.UserRoles;

public interface IUserRoleRepository : IDedsiRepository<UserRole, Guid>;

public class UserRoleRepository(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider)
    : DedsiEfCoreRepository<ProjectNameCQRSDbContext, UserRole, Guid>(dbContextProvider),
        IUserRoleRepository;