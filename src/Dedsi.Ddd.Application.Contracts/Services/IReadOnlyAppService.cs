namespace Dedsi.Ddd.Application.Contracts.Services;


public interface IReadOnlyAppService<in TGetInput, in TGetListInput, TGetOutputDto, TGetListOutputDto> : IDedsiApplicationService
{
    /// <summary>
    /// 查询单个
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TGetOutputDto> GetAsync(TGetInput input);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TGetListOutputDto> GetListAsync(TGetListInput input);
}