using Dedsi.Ddd.Domain.Auditing.Contracts;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Users;
using Microsoft.Extensions.Options;

namespace Dedsi.EntityFrameworkCore.PropertySetters;

public class DedsiAuditPropertySetter : AuditPropertySetter
{
    private readonly DedsiAuditOptions _options;

    // 保留已有构造函数，手动创建与已编译的调用方仍可使用。
    public DedsiAuditPropertySetter(ICurrentUser currentUser, ICurrentTenant currentTenant, IClock clock)
        : this(currentUser, currentTenant, clock, Options.Create(new DedsiAuditOptions())) { }

    public DedsiAuditPropertySetter(ICurrentUser currentUser, ICurrentTenant currentTenant, IClock clock,
        IOptions<DedsiAuditOptions> options) : base(currentUser, currentTenant, clock)
    {
        _options = options.Value;
    }

    public override void SetCreationProperties(object targetObject)
    {
        var creationId = targetObject as IDedsiHasCreationId;
        var tenantMismatch = _options.CheckCreatorTenant && targetObject is IMultiTenant entity && entity.TenantId != CurrentUser.TenantId;
        if (_options.CheckCreatorTenant && tenantMismatch && creationId != null && creationId.CreatorId == Guid.Empty)
            throw new ArgumentException("Cross-tenant creation requires an explicit creator or a matching tenant identity.", nameof(targetObject));
        var creatorId = creationId?.CreatorId is { } existingId && existingId != Guid.Empty
            ? existingId
            : CurrentUser.Id;

        // Dedsi 创建人必须明确；系统任务可以提供系统身份或显式创建人。
        if (creationId != null && (!creatorId.HasValue || creatorId.Value == Guid.Empty))
            throw new ArgumentException("A non-empty creator ID is required. Supply a current user/system identity or an explicit CreatorId.", nameof(targetObject));

        var creationName = targetObject as IDedsiHasCreationName;
        string creatorName = null;
        if (creationName != null && creationName.CreatorName.IsNullOrWhiteSpace())
        {
            // 外部已指定不同创建人时，不把当前操作者的姓名错误地写给该创建人。
            if (creatorId.HasValue && creatorId.Value != Guid.Empty)
            {
                if (creatorId == CurrentUser.Id && (!_options.CheckCreatorTenant || !tenantMismatch))
                    creatorName = !CurrentUser.Name.IsNullOrWhiteSpace() ? CurrentUser.Name : CurrentUser.UserName;
                if (creatorName.IsNullOrWhiteSpace())
                    creatorName = creatorId.Value.ToString("D");
            }
            else
            {
                throw new ArgumentException("A creator identity or an explicit CreatorName is required.", nameof(targetObject));
            }
        }

        // 先校验再赋值，避免身份不完整时留下部分填充的审计字段。
        base.SetCreationProperties(targetObject);
        if (creationId != null && creationId.CreatorId == Guid.Empty)
            ObjectHelper.TrySetProperty(creationId, x => x.CreatorId, () => creatorId.Value);
        if (creatorName != null)
            ObjectHelper.TrySetProperty(creationName, x => x.CreatorName, () => creatorName);
        if (targetObject is IDedsiHasCreationTime creationTime && creationTime.CreationTime == default)
            ObjectHelper.TrySetProperty(creationTime, x => x.CreationTime, () => Clock.Now);
    }
}
