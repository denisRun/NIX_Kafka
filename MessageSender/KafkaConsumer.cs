using Confluent.Kafka;
using MessageSender.DataStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MessageSender
{
    class KafkaConsumer
    {
        private string _topic = "ApiToConsole";
        private ConsumerConfig _config;

        public KafkaConsumer()
        {
            _config = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public async void SubscribeTopic()
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
                        Messages.MarkAsRead(consumer.Message.Value);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Kafka consumer CRASHED");
                    builder.Close();
                }
            }
        }
    }
}
