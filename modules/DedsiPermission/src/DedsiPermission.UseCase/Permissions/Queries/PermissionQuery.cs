using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.Queries;
using DedsiPermission.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DedsiPermission.Permissions.Queries;

public interface IPermissionQuery : IDedsiQuery
{

}

public class PermissionQuery(IDbContextProvider<DedsiPermissionDbContext> dbContextProvider)
    : DedsiEfCoreQuery<DedsiPermissionDbContext>(dbContextProvider), IPermissionQuery
{

}