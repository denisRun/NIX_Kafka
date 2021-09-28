using ConsumerDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerDAL.Repositories
{
    public class MessageRepository : IMessage
    {
        public MessageRepository() { }

        public void AddMessage(string message)
        {
            DataStorage.Messages.AddMessage(message);
        }

        public IEnumerable<string> GetMessages()
        {
            return DataStorage.Messages.GetMessages();
        }
    }
}
