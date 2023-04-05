using System;

namespace DurableFunctionDemo.Activities
{
    internal class ActivityFailedException : Exception
    {
        public ActivityFailedException(string activityName, string message, Exception innerException = null)
            : base(message, innerException)
        {
            ActivityName = activityName;
        }

        public string ActivityName { get; }
    }
}
