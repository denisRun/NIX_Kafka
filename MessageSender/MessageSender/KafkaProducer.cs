using Confluent.Kafka;
using MessageSender.DataStorage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageSender
{
    class KafkaProducer
    {
        private ProducerConfig _config;
        private string _kafkaTopic = "msgSend";

        public KafkaProducer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "127.0.0.1:9092"
            };
        }

        public async void SendMessage()
        {
            Console.WriteLine("To stop messaging ENTER /exit\n");
            using var producer = new ProducerBuilder<Null, string>(_config).Build();

            var message = new Message<Null, string>
            {
                Value = ""
            };

            while(true)
            {
                Console.WriteLine("Enter your message: ");
                message.Value = Console.ReadLine();

                if (message.Value == "/exit")
                {
                    Messages.WriteInFile();
                    Environment.Exit(Environment.ExitCode);
                }

                try
                {
                    Messages.AddMessage(message.Value);
                    var result = producer.ProduceAsync(_kafkaTopic, message);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                message.Value = "";
            }

        }
    }
}
