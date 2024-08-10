using Dedsi.Ddd.CQRS;
using ProjectNameCQRS.Users.Dtos;
using Volo.Abp.Application.Dtos;

namespace ProjectNameCQRS.Users.Commands;

public record SearchUserCommand(string UserName,string Account,string Email) : IDedsiCommand<SearchUserPagedResultDto>;

/// <summary>
/// 查询结果
/// </summary>
public class SearchUserPagedResultDto : PagedResultDto<UserDto>;