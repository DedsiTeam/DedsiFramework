using Volo.Abp.Application.Dtos;

namespace Dedsi.Ddd.Application.Contracts.Dtos;

/// <summary>
/// 分页查询出参
/// </summary>
public class DedsiPagedResultDto<TRowDto> : PagedResultDto<TRowDto>
{

    public DedsiPagedResultDto()
    {
    }


    public DedsiPagedResultDto(long totalCount, IReadOnlyList<TRowDto> items) : base(totalCount, items)
    {
    }
}