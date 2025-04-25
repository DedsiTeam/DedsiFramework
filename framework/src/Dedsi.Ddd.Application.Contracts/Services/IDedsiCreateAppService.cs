using Volo.Abp.Application.Services;

namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IDedsiCreateAppService<in TCreateInput, TCreateOutputDto> : ICreateAppService<TCreateOutputDto, TCreateInput>;