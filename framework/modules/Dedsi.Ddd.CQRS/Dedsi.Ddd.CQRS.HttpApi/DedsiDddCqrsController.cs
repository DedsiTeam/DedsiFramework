using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Dedsi.Ddd.CQRS.HttpApi;

[ApiController]
[Area(DedsiDddCqrsConsts.ApplicationName)]
[Route($"api/{DedsiDddCqrsConsts.ApplicationName}/[controller]")]
[ApiExplorerSettings(GroupName = DedsiDddCqrsConsts.ApplicationName)]
public class DedsiDddCqrsController : DedsiControllerBase;