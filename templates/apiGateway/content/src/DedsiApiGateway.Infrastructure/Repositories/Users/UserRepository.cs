using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.Repositories;
using DedsiApiGateway.EntityFrameworkCore;
using DedsiApiGateway.Users;
using Volo.Abp.EntityFrameworkCore;

namespace DedsiApiGateway.Repositories.Users;

public interface IUserRepository : IDedsiCqrsRepository<User, Guid>;

public class UserRepository(IDbContextProvider<DedsiApiGatewayDbContext> dbContextProvider)
    : DedsiCqrsEfCoreRepository<DedsiApiGatewayDbContext, User, Guid>(dbContextProvider),
        IUserRepository;