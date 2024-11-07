using Application.Contracts.Logging;
using Microsoft.Extensions.Logging;

namespace Infrastructure.LogginService;

public class LoggerAdapter<T> : IApplicationLogger<T>
{
    private readonly ILogger<T> _loggerFactory;

    public LoggerAdapter(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory.CreateLogger<T>();
    }

    public void LogInformation(string message, params object[] args)
    { 
        _loggerFactory.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _loggerFactory.LogWarning(message, args);
    }
}
