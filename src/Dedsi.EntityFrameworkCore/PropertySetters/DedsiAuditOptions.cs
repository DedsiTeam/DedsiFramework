namespace Dedsi.EntityFrameworkCore.PropertySetters;

public class DedsiAuditOptions
{
    /// <summary>
    /// 检查自动填充创建人时的用户与实体租户一致性。默认关闭以兼容现有项目。
    /// 显式创建人仍保留，但跨租户时不自动借用当前用户姓名。
    /// </summary>
    public bool CheckCreatorTenant { get; set; }
}
