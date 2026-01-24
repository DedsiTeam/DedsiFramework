using Dedsi.Ddd.Domain.Auditing.Contracts;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Users;

namespace Dedsi.EntityFrameworkCore.PropertySetters;

public class DedsiAuditPropertySetter(
    ICurrentUser currentUser,
    ICurrentTenant currentTenant,
    IClock clock) 
    : AuditPropertySetter(currentUser, currentTenant, clock)
{
    public override void SetCreationProperties(object targetObject)
    {
        // abp
        base.SetCreationProperties(targetObject);

        if (targetObject is IDedsiHasCreationName dedsiHasCreationName)
        {
            if (CurrentUser.Name.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"CurrentUser.Name 不存在值。");
            }

            ObjectHelper.TrySetProperty(dedsiHasCreationName, x => x.CreatorName, () => CurrentUser.Name);
        }

        if (targetObject is IDedsiHasCreationId dedsiMayHaveCreator)
        {
            if (!CurrentUser.Id.HasValue)
            {
                throw new ArgumentException($"CurrentUser.Id 不存在值。");
            }

            ObjectHelper.TrySetProperty(dedsiMayHaveCreator, x => x.CreatorId, () => CurrentUser.Id);
        }

        if (targetObject is IDedsiHasCreationTime dedsiHasCreationTime)
        {
            ObjectHelper.TrySetProperty(dedsiHasCreationTime, x => x.CreationTime, () => Clock.Now);
        }
    }
}