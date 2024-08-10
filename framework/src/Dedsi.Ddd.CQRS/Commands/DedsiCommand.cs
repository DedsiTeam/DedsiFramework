namespace Dedsi.Ddd.CQRS.Commands;

public class DedsiCommand : IDedsiCommand;

public class DedsiCommand<TResponse> : IDedsiCommand<TResponse>;