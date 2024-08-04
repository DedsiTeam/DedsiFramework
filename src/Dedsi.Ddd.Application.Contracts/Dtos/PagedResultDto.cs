namespace Dedsi.Ddd.Application.Contracts.Dtos;

public class PagedResultDto<T>
{
    public long TotalCount { get; set; }
    
    public IReadOnlyList<T> Items { get; set; }
}