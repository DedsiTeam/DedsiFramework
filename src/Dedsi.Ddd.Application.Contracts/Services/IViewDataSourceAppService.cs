namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IViewDataSourceAppService<T>
{
    /// <summary>
    /// 获得页面数据源
    /// </summary>
    /// <returns></returns>
    Task<T> GetViewDataSourceAsync();
}