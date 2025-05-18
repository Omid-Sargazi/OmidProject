using System.Transactions;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

/// <summary>
///     Abstract base class for command handlers.
/// </summary>
/// <typeparam name="TCommand">The type of command that this handler processes.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the command handler.</typeparam>
public abstract class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
    where TCommand : Command
    where TResponse : CommandResponse
{
    private readonly bool _isTransactional;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandHandler{TCommand, TResponse}" /> class.
    /// </summary>
    /// <param name="isTransactional">Indicates whether the command should be executed within a transaction.</param>
    protected CommandHandler(bool isTransactional = false)
    {
        _isTransactional = isTransactional;
    }

    /// <summary>
    ///     Executes the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="command">The command to execute.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the command response.</returns>
    public async Task<TResponse> Execute(TCommand command, CancellationToken cancellationToken)
    {
        // Check if cancellation has been requested
        if (cancellationToken.IsCancellationRequested) return (TResponse)CommandResponseCanceled.CreateCanceled();

        // If not transactional, directly execute the command
        if (!_isTransactional) return await Executor(command, cancellationToken);

        // Configure TransactionScope for async support
        TransactionOptions transactionOptions = new()
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.MaximumTimeout
        };

        using TransactionScope scope = new(TransactionScopeOption.Required, transactionOptions,
            TransactionScopeAsyncFlowOption.Enabled);

        // Execute the command and wait for completion
        var result = await Executor(command, cancellationToken);

        // Complete the transaction
        scope.Complete();

        return result;
    }

    /// <summary>
    ///     The method that must be implemented in each specific handler for command execution.
    /// </summary>
    /// <param name="command">The command to be executed.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the command response.</returns>
    public abstract Task<TResponse> Executor(TCommand command, CancellationToken cancellationToken);
}