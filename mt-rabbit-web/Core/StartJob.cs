namespace Core
{
    public class StartJob : IStartJob
    {
        public int CorrelationId { get; set; }
        public string JobName { get; set; }
    }
}