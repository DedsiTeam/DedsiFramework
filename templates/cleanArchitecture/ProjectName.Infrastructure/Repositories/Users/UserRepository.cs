using Dedsi.SqlSugar.Repositories;
using ProjectName.Domain.Users;
using SqlSugar;

namespace ProjectName.Infrastructure.Repositories.Users;

public class UserRepository(ISqlSugarClient sqlSugarClient) : Repository<User>(sqlSugarClient),IUserRepository
{
    
}