using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace ProjectNameCQRS;

[ApiController]
[Area(ProjectNameCQRSDomainOptions.ApplicationName)]
[Route("api/ProjectName/[controller]/[action]")]
// [ApiExplorerSettings(GroupName = ProjectNameCQRSDomainOptions.ApplicationName)]
public abstract class ProjectNameController : DedsiControllerBase;