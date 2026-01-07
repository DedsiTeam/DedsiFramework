using Dedsi.Ddd.Domain.Auditing.Contracts;
using Volo.Abp.Domain.Entities;

namespace Dedsi.Ddd.CQRS.CommandEventRecorders;

/// <summary>
/// 命令事件记录器
/// </summary>
public class CommandEventRecorder : Entity<Guid>, IDedsiCreationAuditedObject
{
    protected CommandEventRecorder()
    {

    }

    public CommandEventRecorder(Guid dataId, string name, string fullName, RecorderDataSource dataSource) : base(Guid.CreateVersion7())
    {
        DataId = dataId;
        Name = name;
        FullName = fullName;
        DataSource = dataSource;

        CreationTime = DateTime.Now;
    }

    public string CreatorName { get; private set; }

    public Guid? CreatorId { get; private set; }

    public DateTime CreationTime { get; private set; }



    public Guid DataId { get; private set; }

    public string Name { get; private set; }

    public string FullName { get; private set; }



    /// <summary>
    /// 数据来源
    /// </summary>
    public RecorderDataSource DataSource { get; private set; }
}

/// <summary>
/// 数据来源
/// </summary>
public enum RecorderDataSource
{
    /// <summary>
    /// 命令
    /// </summary>
    Command = 1,
    // 事件
    Event = 2
}
