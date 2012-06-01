using MassTransit;

namespace Core
{
    public interface IStartJob : CorrelatedBy<int>
    {
        string JobName { get; set; }
    }
}