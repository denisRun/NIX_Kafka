using Confluent.Kafka;
using ConsumerBLL.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerBLL.Kafka
{
    public class KafkaConsumer : IHostedService
    {
        private ConsumerConfig _config;
        private string _topic = "ConsoleToApi";
        private IMessageService _messageService;

        public KafkaConsumer(IMessageService messageService)
        {
            _messageService = messageService;
            _config = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "kafka:9093",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var builder = new ConsumerBuilder<Ignore,
                string>(_config).Build())
            {
                builder.Subscribe(_topic);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        Console.WriteLine("ConsumerApi");
                        var consumer = builder.Consume(cancelToken.Token);
                        Console.WriteLine(consumer.Message.Value);
                        await _messageService.AddMessage(consumer.Message.Value);
                    }
                }
                catch (Exception)
                {
                    builder.Close();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
