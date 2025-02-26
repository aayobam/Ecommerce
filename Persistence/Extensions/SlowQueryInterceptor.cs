using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace Persistence.Extensions;

public class SlowQueryInterceptor : DbCommandInterceptor
{
    private const int _slowQueryThreshold = 200; // in milliseconds
    
    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        using ILoggerFactory loggerFactory =  LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = loggerFactory.CreateLogger(nameof(SlowQueryInterceptor));
        if (eventData.Duration.TotalMilliseconds > _slowQueryThreshold)
            logger.LogInformation($"slow query ({eventData.Duration.TotalMilliseconds} ms) : {command.CommandText}");
        return base.ReaderExecuted(command, eventData, result);
    }
}
