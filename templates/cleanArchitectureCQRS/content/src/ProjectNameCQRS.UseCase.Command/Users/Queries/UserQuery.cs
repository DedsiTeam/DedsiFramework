using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using Mapster;
using ProjectNameCQRS.EntityFrameworkCore;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Dtos;
using Volo.Abp.EntityFrameworkCore;

namespace ProjectNameCQRS.Users.Queries;

public interface IUserQuery: IDedsiQuery
{
    Task<SearchUserPagedResultDto> SearchUserAsync(SearchUserCommand command);
}

public class UserQuery(IDbContextProvider<ProjectNameCQRSDbContext> dbContextProvider) 
    : DedsiEfCoreQuery<ProjectNameCQRSDbContext>(dbContextProvider), 
        IUserQuery
{
    public async Task<SearchUserPagedResultDto> SearchUserAsync(SearchUserCommand request)
    {
        var dbContext = await GetDbContextAsync();
        
        // 查询数据库
        var totalCount = dbContext.Users.Count();
        var dbList = dbContext.Users.ToList();

        // 转为 dto
        var items = dbList.Adapt<List<UserDto>>();
        
        return new SearchUserPagedResultDto()
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}