using Dedsi.Ddd.Domain.Repositories;

namespace ProjectName.Domain.Users;

public interface IUserDedsiRepository : IDedsiRepository<User, Guid>
{
    
}