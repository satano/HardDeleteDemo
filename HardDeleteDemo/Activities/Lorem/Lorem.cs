using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DurableFunctionDemo.Activities.Lorem
{
    internal class Lorem
    {
        private readonly IConfiguration _configuration;

        public Lorem(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName(nameof(Lorem))]
        public async Task<HardDeleteResult> HardDelete([ActivityTrigger] HardDeleteInput input, ILogger logger)
        {
            string cnstr = _configuration.GetConnectionString("Receipt");
            logger.LogInformation($"{nameof(Lorem)} connection string: {{cnstr}}", cnstr);
            logger.LogInformation("Hard delete '{hardDeleteActivity}' started. Deleting items older than {itemTtl}.",
                nameof(Lorem), input.Ttl);

            int deletedItemsCount = Random.Shared.Next(500, 2000);
            await Task.Delay(deletedItemsCount);

            logger.LogInformation("Hard delete '{hardDeleteActivity}' finished and deleted {deletedItemsCount} items.",
                nameof(Lorem), deletedItemsCount);

            return new HardDeleteResult(nameof(Lorem), deletedItemsCount);
        }
    }
}
