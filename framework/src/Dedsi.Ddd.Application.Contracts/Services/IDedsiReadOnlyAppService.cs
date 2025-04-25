using Volo.Abp.Application.Services;

namespace Dedsi.Ddd.Application.Contracts.Services;


public interface IDedsiReadOnlyAppService<in TKey, in TGetListInput, TGetOutputDto, TGetListOutputDto> 
    : IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>;