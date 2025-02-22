using Volo.Abp.Application.Dtos;

namespace ProjectNameCQRS.Users.Dtos;

public class UserDto : EntityDto<Guid>
{
    public string UserName { get; set; }

    public string Account { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}