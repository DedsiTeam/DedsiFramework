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
        SetCreatorName(targetObject);
        base.SetCreationProperties(targetObject);
    }

    private void SetCreatorName(object targetObject)
    {
        if (targetObject is IHasCreationName hasCreationNameObject)
        {
            ObjectHelper.TrySetProperty(hasCreationNameObject, x => x.CreatorName, () => CurrentUser.Name);
        }
    }
}