using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerDAL.Interfaces
{
    public interface IMessage
    {
        Task AddMessage(string message);
        IEnumerable<string> GetMessages();
    }
}
