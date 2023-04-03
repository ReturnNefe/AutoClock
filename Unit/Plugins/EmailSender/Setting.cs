using System.Text.Json.Serialization;

namespace EmailSender
{
    public class Setting
    {
        private string mailServer = "";
        private string key = "";
        private string sendUser = "";
        private string receiveUser = "";
        private string title = "";

        [JsonPropertyName("mailServer")]
        public string MailServer { get => mailServer; set => mailServer = value ?? ""; }

        [JsonPropertyName("key")]
        public string Key { get => key; set => key = value ?? ""; }
        
        [JsonPropertyName("sendUser")]
        public string SendUser { get => sendUser; set => sendUser = value ?? ""; }

        [JsonPropertyName("receiveUser")]
        public string ReceiveUser { get => receiveUser; set => receiveUser = value ?? ""; }
        
        [JsonPropertyName("title")]
        public string Title { get => title; set => title = value ?? ""; }
    }
}