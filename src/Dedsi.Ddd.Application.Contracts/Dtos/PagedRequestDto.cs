namespace Dedsi.Ddd.Application.Contracts.Dtos;

public class PagedRequestDto
{

    private int _pageIndex = 1;
    private int _pageSize = 10;
    
    public virtual int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value <= 0 ? 1 : value;
    }
    
    public virtual int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 1000 ? 10 : value <= 0 ? 10 : value;
    }
}