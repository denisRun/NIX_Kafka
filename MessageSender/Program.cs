using Confluent.Kafka;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MessageSender
{
    class Program
    {
        
        static void Main(string[] args)
        {
            KafkaConsumer consumer = new KafkaConsumer();
            Thread threadC = new Thread(consumer.SubscribeTopic);
            threadC.Start();
            KafkaProducer producer = new KafkaProducer();
            Thread threadP = new Thread(producer.SendMessage);
            threadP.Start();
        }
    }
}
