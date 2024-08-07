using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace ProjectName;

[Area(ProjectNameDomainOptions.ModuleName)]
// [RemoteService(Name = ProjectNameDomainOptions.RemoteServiceName)]
[Route("api/ProjectName/[controller]/[action]")]
public abstract class ProjectNameController : DedsiControllerBase
{
    
}