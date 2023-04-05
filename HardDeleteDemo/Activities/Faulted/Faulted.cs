using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DurableFunctionDemo.Activities.Faulted
{
    internal static class Faulted
    {
        [FunctionName(nameof(Faulted))]
        public static async Task<HardDeleteResult> HardDelete([ActivityTrigger] HardDeleteInput input, ILogger logger)
        {
            logger.LogInformation("Hard delete '{hardDeleteActivity}' started.", nameof(Faulted));

            int deletedItemsCount = Random.Shared.Next(500, 2000);
            await Task.Delay(deletedItemsCount);

            throw new ActivityFailedException(
                nameof(Faulted),
                $"Some eerror occured during hard deleting items in activity '{nameof(Faulted)}'.");
        }
    }
}
