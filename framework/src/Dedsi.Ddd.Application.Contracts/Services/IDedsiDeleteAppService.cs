namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IDedsiDeleteAppService<in TKey> : IDedsiApplicationService
{
    Task DeleteAsync(TKey id);
}