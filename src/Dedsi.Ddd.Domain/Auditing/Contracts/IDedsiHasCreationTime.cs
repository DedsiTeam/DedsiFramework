namespace Dedsi.Ddd.Domain.Auditing.Contracts;

public interface IDedsiHasCreationTime
{
    /// <summary>
    /// Creation time.
    /// </summary>
    DateTime CreationTime { get; }
}
