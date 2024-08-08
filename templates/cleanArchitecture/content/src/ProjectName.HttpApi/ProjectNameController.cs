using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace ProjectName;

[ApiController]
[Area(ProjectNameDomainOptions.ApplicationName)]
[Route("api/ProjectName/[controller]/[action]")]
public abstract class ProjectNameController : DedsiControllerBase
{
    
}