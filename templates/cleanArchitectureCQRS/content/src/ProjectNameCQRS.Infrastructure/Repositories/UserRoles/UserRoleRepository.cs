using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Repositories.UserRoles;

public interface IUserRoleRepository : IDedsiCqrsRepository<UserRole, Guid>;

public class UserRoleRepository(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider)
    : DedsiCqrsEfCoreRepository<ProjectNameCQRSDbContext, UserRole, Guid>(dbContextProvider),
        IUserRoleRepository;