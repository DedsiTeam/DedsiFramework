using Volo.Abp.Application.Dtos;

namespace Dedsi.Ddd.Application.Contracts.Dtos;

/// <summary>
/// 分页查询出参
/// </summary>
public class DedsiPagedResultDto<TDto> : PagedResultDto<TDto>
{

    public DedsiPagedResultDto()
    {
    }


    public DedsiPagedResultDto(long totalCount, IReadOnlyList<TDto> items) : base(totalCount, items)
    {
    }
}