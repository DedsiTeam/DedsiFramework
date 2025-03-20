using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace ProjectName;

[ApiController]
[Area(ProjectNameCoreOptions.ApplicationName)]
[Route("api/ProjectName/[controller]/[action]")]
[ApiExplorerSettings(GroupName = ProjectNameCoreOptions.ApplicationName)]
public abstract class ProjectNameController : DedsiControllerBase;