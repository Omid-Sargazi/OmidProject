using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Frameworks.Contracts.Markers;

public interface ICommandHandler<in TCommand, TCommandResponse>
    where TCommand : Command where TCommandResponse : CommandResponse
{
    Task<TCommandResponse> Execute(TCommand command, CancellationToken cancellationToken);
}