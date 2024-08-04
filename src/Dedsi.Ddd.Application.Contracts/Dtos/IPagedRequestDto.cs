namespace Dedsi.Ddd.Application.Contracts.Dtos;

public interface IPagedRequestDto
{
    int PageIndex { get; set; }
    
    int PageSize { get; set; }
}