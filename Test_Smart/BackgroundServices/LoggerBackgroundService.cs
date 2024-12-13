using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Test_Smart.BackgroundServices;

public class LoggerBackgroundService : BackgroundService
{
    private readonly ILogger<LoggerBackgroundService> _logger;

    private readonly ConcurrentQueue<string> _taskQueue = new();

    public LoggerBackgroundService(ILogger<LoggerBackgroundService> logger)
    {
        _logger = logger;
    }

    public void Log(string message)
    {
        _taskQueue.Enqueue(message);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Logger Background Service is running.");

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_taskQueue.TryDequeue(out var message))
            {
                _logger.LogInformation($"Processing log: {message}");
                await Task.Delay(500, stoppingToken); 
            }
            else
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        _logger.LogInformation("Logger Background Service is stopping.");
    }
}