using ApiGateway.AgRouteConfigs;
using ApiGateway.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ApiGateway.Repositories;

public interface IAgRouteConfigRepository : IRepository<AgRouteConfig>;

public class AgRouteConfigRepository(IDbContextProvider<ApiGatewayDbContext> dbContextProvider)
    : EfCoreRepository<ApiGatewayDbContext, AgRouteConfig>(dbContextProvider), IAgRouteConfigRepository;