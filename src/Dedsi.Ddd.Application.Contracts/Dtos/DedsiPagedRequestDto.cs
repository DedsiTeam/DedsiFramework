using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Dedsi.Core.Paging;

namespace Dedsi.Ddd.Application.Contracts.Dtos;

/// <summary>
/// 分页查询入参
/// </summary>
public class DedsiPagedRequestDto : IValidatableObject
{
    /// <summary>
    /// 当前第几页
    /// </summary>
    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value < 1 ? DedsiPaging.DefaultPageIndex : value;
    }
    private int _pageIndex = DedsiPaging.DefaultPageIndex;

    /// <summary>
    /// 每页多少条
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 || value > DedsiPaging.MaxPageSize ? DedsiPaging.DefaultPageSize : value;
    }
    private int _pageSize = DedsiPaging.DefaultPageSize;

    /// <summary>
    /// 是否导出：仅由服务端调用 EnableExport 开启，不接受客户端绑定。
    /// </summary>
    [JsonIgnore]
    public bool IsExport { get; private set; }

    /// <summary>
    /// 由服务端完成导出权限校验后调用。
    /// </summary>
    public void EnableExport() => IsExport = true;

    /// <summary>
    /// 通过标准 DTO 验证提前拒绝超出查询范围的分页请求。
    /// </summary>
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        if (((long)PageIndex - 1) * PageSize > int.MaxValue)
            yield return new ValidationResult("分页偏移量过大，请减小页码。", new[] { nameof(PageIndex) });
    }

    /// <summary>
    /// 计算偏移量，不修改分页属性；偏移量溢出时抛出 ArgumentOutOfRangeException。
    /// </summary>
    public int GetSkipCount() => DedsiPaging.GetSkipCount(PageIndex, PageSize);
}
