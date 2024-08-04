namespace Dedsi.Ddd.Application.Contracts.Services;

public interface IDeleteAppService<in TKey> : IDedsiApplicationService
{
    Task DeleteAsync(TKey id);
}