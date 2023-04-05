using System.Collections.Generic;

namespace DurableFunctionDemo.Activities
{
    public class HardDeleteResults
    {
        public class OkResult
        {
            public OkResult(string name, int deletedItems)
            {
                Name = name;
                DeletedItems = deletedItems;
            }

            public string Name { get; }
            public int DeletedItems { get; }
        }

        public class ErrorResult
        {
            public ErrorResult(string name, string error)
            {
                Name = name;
                Error = error;
            }

            public string Name { get; }
            public string Error { get; }
        }

        public void AddResult(string name, int deletedItems)
            => Results.Add(new OkResult(name, deletedItems));

        public void AddError(string name, string error)
            => Errors.Add(new ErrorResult(name, error));

        public List<OkResult> Results { get; } = new List<OkResult>();
        public List<ErrorResult> Errors { get; } = new List<ErrorResult>();
    }
}
