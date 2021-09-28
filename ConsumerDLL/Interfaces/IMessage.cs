using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerDAL.Interfaces
{
    public interface IMessage
    {
        void AddMessage(string message);
        IEnumerable<string> GetMessages();
    }
}
