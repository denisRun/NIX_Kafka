using ConsumerBLL.Interfaces;
using ConsumerBLL.Kafka;
using ConsumerDAL.Interfaces;
using ConsumerDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerBLL.Services
{
    public class MessageService : IMessageService
    {
        private IMessage _messageRepository;

        public MessageService(IMessage messageResository)
        {
            _messageRepository = messageResository;
        }

        public async Task AddMessage(string message)
        {
            await _messageRepository.AddMessage(message);
        }

        public async Task<IEnumerable<string>> GetMessages()
        {
            var msgList = _messageRepository.GetMessages();

            if (msgList!=null)
            {
                var producer = new KafkaProducer();
                await producer.SendMessagesToKafkaAsync(msgList);
            }

            return msgList;
        }
    }
}
