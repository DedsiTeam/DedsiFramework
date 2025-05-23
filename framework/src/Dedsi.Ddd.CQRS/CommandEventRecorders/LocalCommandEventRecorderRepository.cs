using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Dedsi.Ddd.CQRS.CommandEventRecorders;

public class LocalCommandEventRecorderRepository(ILogger<LocalCommandEventRecorderRepository> logger) : ICommandEventRecorderRepository
{
    public Task<CommandEventRecorder[]> GetByDataIdAsync(Guid dataId)
    {
        logger.LogInformation($"GetByDataIdAsync() dataId = {dataId}");

        return Task.FromResult(new CommandEventRecorder[0]);
    }

    public Task<(long, CommandEventRecorder[])> GetPagedListAsync(int skipCount, int maxResultCount, string name, string creatorName)
    {
        logger.LogInformation($"GetPagedListAsync() dataId = {skipCount}");
        logger.LogInformation($"GetPagedListAsync() maxResultCount = {maxResultCount}");
        logger.LogInformation($"GetPagedListAsync() name = {name}");
        logger.LogInformation($"GetPagedListAsync() creatorName = {creatorName}");

        return Task.FromResult(((long)0, new CommandEventRecorder[0]));
    }

    public Task InsertAsync(Guid dataId, string name, string fullName, RecorderDataSource dataSource, CancellationToken cancellationToken)
    {
        logger.LogInformation($"InsertAsync() dataId = {dataId}");
        logger.LogInformation($"InsertAsync() name = {name}");
        logger.LogInformation($"InsertAsync() fullName = {fullName}");
        logger.LogInformation($"InsertAsync() dataSource = {dataSource}");

        return Task.CompletedTask;
    }

    public Task InsertAsync(CommandEventRecorder entity, CancellationToken cancellationToken)
    {
        logger.LogInformation(JsonSerializer.Serialize(entity));

        return Task.CompletedTask;
    }
}
