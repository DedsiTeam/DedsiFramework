using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Repositories.Users;

public interface IUserRepository : IDedsiRepository<User, Guid>;

public class UserRepository(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider)
    : DedsiEfCoreRepository<ProjectNameCQRSDbContext, User, Guid>(dbContextProvider), 
        IUserRepository;