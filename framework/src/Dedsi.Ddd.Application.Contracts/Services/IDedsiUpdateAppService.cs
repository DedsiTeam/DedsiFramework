using Volo.Abp.Application.Services;

namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IDedsiUpdateAppService<in TKey,in TUpdateInput, TUpdateOutputDto> : IUpdateAppService<TUpdateOutputDto,TKey, TUpdateInput>;