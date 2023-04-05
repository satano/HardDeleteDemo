using DurableFunctionDemo.Activities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DurableFunctionDemo
{
    public static class Orchestrator
    {
        [FunctionName(nameof(RunOrchestrator))]
        public static async Task<HardDeleteResults> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {

            List<Task<HardDeleteResult>> tasks = new();
            foreach (string activity in ActivityList.GetAllActivities())
            {
                tasks.Add(context.CallActivityAsync<HardDeleteResult>(activity, new HardDeleteInput(30)));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch
            {
            }

            HardDeleteResults result = new();
            foreach (Task<HardDeleteResult> task in tasks)
            {
                if (task.IsFaulted)
                {
                    ActivityFailedException aex = (ActivityFailedException)task.Exception.InnerException.InnerException;
                    result.AddError(aex.ActivityName, aex.Message);
                }
                else
                {
                    result.AddResult(task.Result.Name, task.Result.ItemsDeleted);
                }
            }
            return result;
        }

        [FunctionName("HardDeleteStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync(nameof(RunOrchestrator), null);

            log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}