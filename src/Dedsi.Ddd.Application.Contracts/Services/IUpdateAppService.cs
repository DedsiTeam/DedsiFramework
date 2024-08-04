namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IUpdateAppService<in TUpdateInput, TUpdateOutputDto> : IDedsiApplicationService
{
    Task<TUpdateOutputDto> UpdateAsync(TUpdateInput input);
}