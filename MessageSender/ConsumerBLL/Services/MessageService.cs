using ConsumerBLL.Interfaces;
using ConsumerBLL.Kafka;
using ConsumerDAL.Interfaces;
using ConsumerDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsumerBLL.Services
{
    public class MessageService : IMessageService
    {
        private IMessage _messageRepository;

        public MessageService(IMessage messageResository)
        {
            _messageRepository = messageResository;
        }

        public void AddMessage(string message)
        {
            _messageRepository.AddMessage(message);
        }

        public List<string> GetMessages()
        {
            var msgList = _messageRepository.GetMessages();

            if (msgList.Count > 0)
            {
                var producer = new Producer();
                producer.SendMessagesToKafka(msgList);
            }

            return msgList;
        }
    }
}
