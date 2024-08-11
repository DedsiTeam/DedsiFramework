using Volo.Abp.Application.Dtos;

namespace ProjectNameCQRS.Users.Dtos;

/// <summary>
/// 查询结果
/// </summary>
public class SearchUserPagedResultDto : PagedResultDto<UserDto>;