using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace DedsiPermission;

[ApiController]
[Area(DedsiPermissionDomainConsts.ApplicationName)]
[Route("api/DedsiPermission/[controller]/[action]")]
[ApiExplorerSettings(GroupName = DedsiPermissionDomainConsts.ApplicationName)]
public abstract class DedsiPermissionController : DedsiControllerBase;