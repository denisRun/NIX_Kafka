using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerBLL.Interfaces
{
    public interface IMessageService
    {
        Task AddMessage(string message);
        Task<IEnumerable<string>> GetMessages();
    }
}
