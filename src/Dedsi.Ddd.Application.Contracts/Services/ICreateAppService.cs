namespace Dedsi.Ddd.Application.Contracts.Services;

public interface ICreateAppService<in TCreateInput, TCreateOutputDto> : IDedsiApplicationService
{
    Task<TCreateOutputDto> CreateAsync(TCreateInput input);
}