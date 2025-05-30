using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace DedsiApiGateway;

[ApiController]
[Area(DedsiApiGatewayDomainConsts.ApplicationName)]
[Route("api/DedsiApiGateway/[controller]/[action]")]
[ApiExplorerSettings(GroupName = DedsiApiGatewayDomainConsts.ApplicationName)]
public abstract class DedsiApiGatewayController : DedsiControllerBase;