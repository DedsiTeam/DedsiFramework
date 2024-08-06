using Dedsi.EntityFrameworkCore.Repositories;
using ProjectName.Domain.Users;
using ProjectName.Infrastructure.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectName.Infrastructure.Repositories.Users;

public class UserRepository(IDbContextProvider<ProjectNameDbContext> dbContextProvider) : DedsiEfCoreRepository<ProjectNameDbContext,User,Guid>(dbContextProvider),IUserDedsiRepository;