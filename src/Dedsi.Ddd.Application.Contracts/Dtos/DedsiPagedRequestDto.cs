using System.Text.Json.Serialization;

namespace Dedsi.Ddd.Application.Contracts.Dtos;

/// <summary>
/// 分页查询入参
/// </summary>
public class DedsiPagedRequestDto
{
    /// <summary>
    /// 当前第几页
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 每页多少条
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 是否导出：注意需要在Controller中设置为true，才能导出数据,前端无需赋值
    /// </summary>
    [JsonIgnore]
    public bool IsExport { get; set; } = false;

    public int GetSkipCount()
    {
        if (PageIndex < 1) { PageIndex = 1; }
        if (PageSize > 1000 || PageSize < 1) { PageSize = 10; }

        return (PageIndex - 1) * PageSize;
    }

}

