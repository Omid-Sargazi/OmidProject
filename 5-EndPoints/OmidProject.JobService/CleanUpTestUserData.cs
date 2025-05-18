using OmidProject.Applications.Contracts.Repository;
using OmidProject.Domains.Domain.Project;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OmidProject.JobService;

public class CleanUpTestUserData : IHostedService, IDisposable, IJobService
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly ILogger<CleanUpTestUserData> _logger;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CleanUpTestUserData(
        IServiceScopeFactory scopeFactory,
        ILogger<CleanUpTestUserData> logger)
    {
        _serviceScopeFactory = scopeFactory;
        _logger = logger;
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{DateTime.Now}: {nameof(CleanUpTestUserData)} Started.");
        ScheduleNextExecution();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{DateTime.Now}: {nameof(CleanUpTestUserData)} Stopping.");
        await _cancellationTokenSource.CancelAsync();
    }

    private async void ScheduleNextExecution()
    {
        try
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextExecuting = DateTime.Today.AddDays(1);
                var delay = nextExecuting - now;

                _logger.LogInformation($"{DateTime.Now}: {nameof(CleanUpTestUserData)}, next execution scheduled at {nextExecuting}");

                await Task.Delay(delay);

                ExecuteJon();
            }
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation($"{DateTime.Now}: {nameof(CleanUpTestUserData)} Canceled.");
        }
        catch (Exception e)
        {
            _logger.LogError($"{DateTime.Now}: {nameof(CleanUpTestUserData)} Error => {e.Message}");
        }
    }

    private void ExecuteJon()
    {
        _logger.LogInformation($"{DateTime.Now}: {nameof(CleanUpTestUserData)}, Executing ...");

        using var scope = _serviceScopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Project, int>>();
        var data = repository.CountAsync();
    }
}