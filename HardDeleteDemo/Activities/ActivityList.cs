using System.Collections.Generic;

namespace DurableFunctionDemo.Activities
{
    internal static class ActivityList
    {
        public static IEnumerable<string> GetAllActivities()
        {
            yield return nameof(Lorem);
            yield return nameof(Faulted);
            yield return nameof(Ipsum);
        }
    }
}
