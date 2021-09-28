using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerBLL.Kafka
{
    class KafkaProducer
    {
        private ProducerConfig _config;
        private string _kafkaTopic = "ApiToConsole";
        private Message<Null, String> _message;

        public KafkaProducer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "kafka:9093"
            };
            _message = new Message<Null, string>
            {
                Value = ""
            };
        }

        public async Task SendMessagesToKafkaAsync(IEnumerable<string> messages)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();

            foreach (var i in messages)
            {

                _message.Value = i;
                try
                {
                    var result = await producer.ProduceAsync(_kafkaTopic, _message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
