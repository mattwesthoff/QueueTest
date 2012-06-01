using MassTransit;

namespace core
{
    public class Request : CorrelatedBy<int> 
    {
        public int CorrelationId { get; set; }
        public string Text { get; set;  }
    }

    public class Response : CorrelatedBy<int>
    {
        public int CorrelationId { get; set; }
        public bool Successful { get; set; }
    }
}
