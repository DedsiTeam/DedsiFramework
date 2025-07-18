using Microsoft.Extensions.Logging;

namespace Dedsi.Ddd.CQRS.CommandEventRecorders;

public class CqrsCeRecorder(
    ILogger<CqrsCeRecorder> logger,
    ICommandEventRecorderRepository commandEventRecorderRepository) 
    : ICqrsCeRecorder
{
    /// <inheritdoc />
    public Task RecorderAsync(Guid dataId, string name, string fullName, RecorderDataSource dataSource, CancellationToken cancellationToken)
    {
        name = name.Trim().Replace("`1", "");

        logger.LogInformation("---------------------------------------- CqrsCeRecorder RecorderAsync() -------------------------------------------------------");
        logger.LogInformation($"dataId = {dataId}, name = {name}, fullName = {fullName}, dataSource = {dataSource}.");
        logger.LogInformation("---------------------------------------- CqrsCeRecorder RecorderAsync() -------------------------------------------------------");

        return commandEventRecorderRepository.InsertAsync(dataId, name, fullName, dataSource, cancellationToken);
    }
}
