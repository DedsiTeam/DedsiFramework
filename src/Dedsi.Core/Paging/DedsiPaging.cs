namespace Dedsi.Core.Paging;

/// <summary>
/// 分页边界和偏移量计算，供 DTO 和仓储共用。
/// </summary>
public static class DedsiPaging
{
    public const int DefaultPageIndex = 1;
    public const int DefaultPageSize = 10;
    public const int MaxPageSize = 1000;

    /// <summary>
    /// 校验页码、页大小并计算偏移量；无法表示为 int 时拒绝请求。
    /// </summary>
    public static int GetSkipCount(int pageIndex, int pageSize)
    {
        if (pageIndex < 1)
            throw new ArgumentOutOfRangeException(nameof(pageIndex), pageIndex, "Page index must be at least 1.");
        if (pageSize < 1 || pageSize > MaxPageSize)
            throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, $"Page size must be between 1 and {MaxPageSize}.");

        var skipCount = ((long)pageIndex - 1) * pageSize;
        if (skipCount > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(pageIndex), pageIndex, "Page offset exceeds Int32.MaxValue.");

        return (int)skipCount;
    }
}
