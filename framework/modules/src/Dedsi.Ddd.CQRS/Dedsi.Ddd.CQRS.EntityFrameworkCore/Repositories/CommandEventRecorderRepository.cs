using Dedsi.Ddd.CQRS.CommandEventRecorders;
using Dedsi.Ddd.CQRS.EntityFrameworkCore.EntityFrameworkCores;
using Dedsi.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.Ddd.CQRS.EntityFrameworkCore.Repositories;


public class CommandEventRecorderRepository(IDbContextProvider<DedsiDddCqrsDbContext> dbContextProvider)
    : DedsiCqrsEfCoreRepository<DedsiDddCqrsDbContext, CommandEventRecorder, Guid>(dbContextProvider), ICommandEventRecorderRepository
{
    /// <inheritdoc />
    public Task InsertAsync(CommandEventRecorder entity, CancellationToken cancellationToken)
    {
        return InsertAsync(entity, false);
    }

    /// <inheritdoc />
    public Task InsertAsync(Guid dataId, string name, string fullName, RecorderDataSource dataSource, CancellationToken cancellationToken)
    {
        return InsertAsync(new CommandEventRecorder(dataId, name, fullName, dataSource));
    }

    /// <inheritdoc />
    public async Task<(long, CommandEventRecorder[])> GetPagedListAsync(
        int skipCount,
        int maxResultCount, 
        string? name, 
        string? creatorName)
    {
        var dbContext = await GetDbContextAsync();

        var query = dbContext
            .CommandEventRecorders
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name.Contains(name!) || x.FullName.Contains(name!))
            .WhereIf(!string.IsNullOrWhiteSpace(creatorName), x => x.CreatorName != null && x.CreatorName.Contains(creatorName!));
    
        var totalCount = await query.CountAsync();
        var items = await query.OrderByDescending(a => a.CreationTime).PageBy(skipCount, maxResultCount).ToArrayAsync();

        return (totalCount, items);
    }

    /// <inheritdoc />
    public async Task<CommandEventRecorder[]> GetByDataIdAsync(Guid dataId)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext
                    .CommandEventRecorders
                    .Where(a => a.DataId == dataId)
                    .OrderBy(a => a.CreationTime)
                    .ToArrayAsync();
    }
}
