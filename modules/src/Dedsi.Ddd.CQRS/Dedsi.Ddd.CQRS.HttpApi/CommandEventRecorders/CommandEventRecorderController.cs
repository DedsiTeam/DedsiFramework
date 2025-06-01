using Dedsi.Ddd.CQRS.CommandEventRecorders;
using Microsoft.AspNetCore.Mvc;

namespace Dedsi.Ddd.CQRS.HttpApi.CommandEventRecorders;

public class CommandEventRecorderController(ICommandEventRecorderRepository commandEventRecorderRepository) : DedsiDddCqrsController
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("{pageIndex}/{pageSize}")]
    public async Task<GetPagedListResultDto> GetPagedListAsync([FromRoute] int pageIndex, [FromRoute] int pageSize, [FromBody] GetPagedListInputDto input)
    {
        var result = await commandEventRecorderRepository.GetPagedListAsync(
            (pageIndex - 1) * pageSize,
            pageSize,
            input.Name,
            input.CreatorName
        );

        return new GetPagedListResultDto(result.Item1, result.Item2);
    }

    [HttpGet("ByDataId/{dataId}")]
    public Task<CommandEventRecorder[]> GetByDataIdAsync([FromRoute] Guid dataId)
    {
        return commandEventRecorderRepository.GetByDataIdAsync(dataId);
    }
}

public record GetPagedListInputDto(string? Name, string? CreatorName);

public record GetPagedListResultDto(long TotalCount, CommandEventRecorder[] Items);
