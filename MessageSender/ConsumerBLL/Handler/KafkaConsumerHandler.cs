using Confluent.Kafka;
using ConsumerBLL.Interfaces;
using ConsumerBLL.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerBLL.Handler
{
    public class KafkaConsumerHandler : IHostedService
    {
        private ConsumerConfig _config;
        private string _topic = "msgSend";
        private IMessageService _messageService;

        public KafkaConsumerHandler(IMessageService messageService)
        {
            _messageService = messageService;
            _config = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }
        //private MessageService _messageService = new MessageService();

        public Task StartAsync(CancellationToken cancellationToken)
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
                        var consumer = builder.Consume(cancelToken.Token);
                        _messageService.AddMessage(consumer.Message.Value);
                    }
                }
                catch (Exception)
                {
                    builder.Close();
                }
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
