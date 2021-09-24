using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerBLL.Interfaces
{
    public interface IMessageService
    {
        void AddMessage(string message);
        List<string> GetMessages();
    }
}
