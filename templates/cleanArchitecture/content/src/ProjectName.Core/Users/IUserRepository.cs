using Dedsi.Ddd.Domain.Repositories;

namespace ProjectName.Users;

public interface IUserRepository : IDedsiRepository<User, Guid>;