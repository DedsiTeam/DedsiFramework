using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace DedsiIdentity;

[ApiController]
[Area(DedsiIdentityDomainConsts.ApplicationName)]
[Route("api/DedsiIdentity/[controller]/[action]")]
[ApiExplorerSettings(GroupName = DedsiIdentityDomainConsts.ApplicationName)]
public abstract class DedsiIdentityController : DedsiControllerBase;