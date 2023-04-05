namespace DurableFunctionDemo.Activities
{
    public class HardDeleteInput
    {
        public HardDeleteInput(int ttl)
        {
            Ttl = ttl;
        }

        public int Ttl { get; }
    }
}
