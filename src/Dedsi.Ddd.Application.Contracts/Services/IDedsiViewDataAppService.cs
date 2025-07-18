namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IDedsiViewDataAppService<T>: IDedsiApplicationService
{
    /// <summary>
    /// 获得页面数据源
    /// </summary>
    /// <returns></returns>
    Task<T> GetViewDataSourceAsync();
}