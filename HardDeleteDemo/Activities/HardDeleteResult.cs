namespace DurableFunctionDemo.Activities
{
    public class HardDeleteResult
    {
        public HardDeleteResult(string name, int itemsDeleted)
        {
            Name = name;
            ItemsDeleted = itemsDeleted;
        }

        public string Name { get; }
        public int ItemsDeleted { get; }
    }
}
