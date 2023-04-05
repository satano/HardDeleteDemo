using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DurableFunctionDemo.Activities.Ipsum
{
    internal class Ipsum
    {
        private readonly IConfiguration _configuration;

        public Ipsum(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName(nameof(Ipsum))]
        public async Task<HardDeleteResult> HardDelete([ActivityTrigger] HardDeleteInput input, ILogger logger)
        {
            string cnstr = _configuration.GetConnectionString("Default");
            logger.LogInformation($"{nameof(Ipsum)} connection string: {{cnstr}}", cnstr);
            logger.LogInformation("Hard delete '{hardDeleteActivity}' started. Deleting items older than {itemTtl}.",
                nameof(Ipsum), input.Ttl);

            int deletedItemsCount = Random.Shared.Next(500, 2000);
            await Task.Delay(deletedItemsCount);

            logger.LogInformation("Hard delete '{hardDeleteActivity}' finished and deleted {deletedItemsCount} items.",
                nameof(Ipsum), deletedItemsCount);

            return new HardDeleteResult(nameof(Ipsum), deletedItemsCount);
        }
    }
}
