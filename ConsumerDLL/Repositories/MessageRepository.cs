using ConsumerDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerDAL.Repositories
{
    public class MessageRepository : IMessage
    {
        public MessageRepository() { }

        public async Task AddMessage(string message)
        {
            await Task.Run( () =>
                        DataStorage.Messages.AddMessage(message));
        }

        public IEnumerable<string> GetMessages()
        {
            return DataStorage.Messages.GetMessages();
        }
    }
}
