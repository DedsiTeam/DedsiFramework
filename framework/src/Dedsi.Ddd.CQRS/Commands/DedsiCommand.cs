namespace Dedsi.Ddd.CQRS.Commands;

public record DedsiCommand : IDedsiCommand;

public record DedsiCommand<TResponse> : IDedsiCommand<TResponse>;