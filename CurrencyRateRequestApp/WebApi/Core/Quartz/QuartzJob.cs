using System.Threading.Tasks;
using Quartz;
using WebApi.Core.MassTransit.Producer;

namespace WebApi.Core.Quartz
{
    [DisallowConcurrentExecution]
    public class QuartzJob : IJob
    {
        private Producer _producer;

        public QuartzJob(Producer producer)
        {
            _producer = producer;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _producer.ProducerPublish();

            return Task.CompletedTask;
        }
    }
}
