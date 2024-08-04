namespace Dedsi.Ddd.Application.Contracts.Dtos;

public interface IEntityDto
{

}

public interface IEntityDto<TKey> : IEntityDto
{
    TKey Id { get; set; }
}
