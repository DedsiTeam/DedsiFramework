using Dedsi.EntityFrameworkCore.Repositories;
using ProjectName.EntityFrameworkCore;
using ProjectName.Users;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectName.Repositories.Users;

public class UserRepository(IDbContextProvider<ProjectNameDbContext> dbContextProvider)
    : DedsiEfCoreRepository<ProjectNameDbContext, User, Guid>(dbContextProvider), 
        IUserRepository;