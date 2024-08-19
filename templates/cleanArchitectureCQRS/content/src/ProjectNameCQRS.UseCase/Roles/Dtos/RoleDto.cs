using Volo.Abp.Application.Dtos;

namespace ProjectNameCQRS.Roles.Dtos;

public class RoleDto : EntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string RoleCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string RoleName { get; set; }
}

public class CreateRoleInputDto
{
    /// <summary>
    /// 
    /// </summary>
    public string RoleCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string RoleName { get; set; }
}

public class UpdateRoleInputDto : RoleDto
{

}