using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace MessageSender.DataStorage
{
    public static class Messages
    {
        private static Dictionary<string, bool> _data = new Dictionary<string, bool>();
        private static string _fileName = "MessageJournal";

        public static void AddMessage(string message)
        {
            if (!_data.ContainsKey(message))
            {
                _data[message] = false;
            }
        }

        public static void MarkAsRead(string message)
        {
            if (_data.ContainsKey(message))
            {
                _data[message] = true;
            }
        }

        public static async void WriteInFile()
        {
            //var jsonString = JsonSerializer.Serialize(_data);
            //File.WriteAllText(_fileName, jsonString);
            using FileStream createStream = File.Create(_fileName);
            await JsonSerializer.SerializeAsync(createStream, _data);
        }
    }
}
