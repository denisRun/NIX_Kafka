using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsumerDAL.DataStorage
{
    public static class Messages
    {
        private static Dictionary<string, bool> data = new Dictionary<string, bool>();

        public static void AddMessage(string message)
        {
            if (!data.ContainsKey(message))
            {
                data[message] = false;
            }
        }

        public static List<string> GetMessages()
        {
            List<string> msgList = data.Keys.ToList<string>();

            foreach (var i in msgList)
            {
                data[i] = true;
            }
            return msgList;
        }
    }
}
