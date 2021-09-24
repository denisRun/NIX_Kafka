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
        public async void SubscribeTopic()
        {
            string topic = "ReadMessages";
            var conf = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using (var builder = new ConsumerBuilder<Ignore,
                string>(conf).Build())
            {
                builder.Subscribe(topic);
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
