using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerBLL.Kafka
{
    class Producer
    {
        private ProducerConfig _config;
        private string _kafkaTopic = "ReadMessages";
        private Message<Null, String> _message;

        public Producer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "127.0.0.1:9092"
            };
            _message = new Message<Null, string>
            {
                Value = ""
            };
        }

        public void SendMessagesToKafka(List<string> messages)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();

            foreach (var i in messages)
            {

                _message.Value = i;
                try
                {
                    var result = producer.ProduceAsync(_kafkaTopic, _message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
