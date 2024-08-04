namespace Dedsi.Ddd.Application.Contracts.Services;

public interface ICrudAppService<in TKey, in TGetInput, in TGetListInput, TGetOutputDto, TGetListOutputDto,in TCreateInput, TCreateOutputDto, in TUpdateInput, TUpdateOutputDto>
    : IReadOnlyAppService<TGetInput, TGetListInput, TGetOutputDto, TGetListOutputDto>,
        ICreateUpdateAppService<TCreateInput, TCreateOutputDto, TUpdateInput, TUpdateOutputDto>,
        IDeleteAppService<TKey>
{
    
}