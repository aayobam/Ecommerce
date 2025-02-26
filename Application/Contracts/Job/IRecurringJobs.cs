using Hangfire;
using Hangfire.Server;

namespace Application.Contracts.EventBus;

public interface IRecurringJobs
{
    [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    Task ExecuteSendMail(PerformContext context);
}
