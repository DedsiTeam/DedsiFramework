using Dedsi.EntityFrameworkCore.Repositories;
using ProjectName.Domain.Users;

namespace ProjectName.Infrastructure.Repositories.Users;

public class UserSqlSugarRepository : EfCoreRepository<User>,IUserRepository
{
    
}