using Dedsi.Ddd.CQRS.Commands;
using MediatR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Validation;

namespace Dedsi.Ddd.CQRS.CommandHandlers;


public interface IDedsiCommandHandler : ITransientDependency, IValidationEnabled;

public interface IDedsiCommandHandler<in TCommand> : IDedsiCommandHandler, IRequestHandler<TCommand> where TCommand : IDedsiCommand;

public interface IDedsiCommandHandler<in TCommand, TResponse> : IDedsiCommandHandler, IRequestHandler<TCommand, TResponse> where TCommand : IDedsiCommand<TResponse>;