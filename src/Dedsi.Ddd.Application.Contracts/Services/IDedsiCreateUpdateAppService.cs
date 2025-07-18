namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IDedsiCreateUpdateAppService<in TKey,in TCreateInput, TCreateOutputDto, in TUpdateInput, TUpdateOutputDto>
    : IDedsiCreateAppService<TCreateInput, TCreateOutputDto>,
    IDedsiUpdateAppService<TKey, TUpdateInput, TUpdateOutputDto>;