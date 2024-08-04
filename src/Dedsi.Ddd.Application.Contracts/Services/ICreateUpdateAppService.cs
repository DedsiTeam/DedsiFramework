namespace Dedsi.Ddd.Application.Contracts.Services;

public interface ICreateUpdateAppService<in TCreateInput, TCreateOutputDto, in TUpdateInput, TUpdateOutputDto>
    : ICreateAppService<TCreateInput, TCreateOutputDto>,
    IUpdateAppService<TUpdateInput, TUpdateOutputDto>
{
    
}