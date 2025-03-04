using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace ProjectNameCQRS;

[ApiController]
[Area(ProjectNameCQRSDomainOptions.ApplicationName)]
[Route("api/ProjectNameCQRS/[controller]/[action]")]
// [ApiExplorerSettings(GroupName = ProjectNameCQRSDomainOptions.ApplicationName)]
public abstract class ProjectNameCQRSController : DedsiControllerBase;