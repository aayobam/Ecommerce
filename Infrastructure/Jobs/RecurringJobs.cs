using Application.Contracts.EventBus;
using Hangfire.Server;

namespace Infrastructure.Jobs;

public class RecurringJobs : IRecurringJobs
{
    public Task ExecuteSendMail(PerformContext context)
    {
        throw new NotImplementedException();
    }
}
