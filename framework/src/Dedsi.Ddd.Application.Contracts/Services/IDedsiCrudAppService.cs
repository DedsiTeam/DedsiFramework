namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IDedsiCrudAppService<in TKey,in TGetListInput, TGetOutputDto, TGetListOutputDto,in TCreateInput, TCreateOutputDto, in TUpdateInput, TUpdateOutputDto, TViewData>
    : IDedsiReadOnlyAppService<TKey, TGetListInput, TGetOutputDto, TGetListOutputDto>,
        IDedsiCreateUpdateAppService<TKey, TCreateInput, TCreateOutputDto, TUpdateInput, TUpdateOutputDto>,
        IDedsiDeleteAppService<TKey>,
        IDedsiViewDataAppService<TViewData>;