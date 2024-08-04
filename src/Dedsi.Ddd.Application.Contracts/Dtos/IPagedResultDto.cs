namespace Dedsi.Ddd.Application.Contracts.Dtos;

public interface IPagedResultDto<T>
{
    long TotalCount { get; set; }
    
    IReadOnlyList<T> Items { get; set; }
}