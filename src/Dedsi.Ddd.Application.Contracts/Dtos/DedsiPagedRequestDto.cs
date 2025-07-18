﻿using Volo.Abp.Application.Dtos;

namespace Dedsi.Ddd.Application.Contracts.Dtos;

/// <summary>
/// 分页查询入参
/// </summary>
public class DedsiPagedRequestDto : PagedResultRequestDto
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

    public override int MaxResultCount => PageSize;

    public override int SkipCount => (PageIndex - 1) * PageSize;

    /// <summary>
    /// 是否导出：注意需要在Controller中设置为true，才能导出数据,前端无需赋值
    /// </summary>
    public bool IsExport { get; set; } = false;
}

