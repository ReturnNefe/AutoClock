using System.Text.Json.Serialization;

namespace Luogu
{
    public class Setting
    {
        public class UserSetting
        {
            private string cookie = "";
            
            [JsonPropertyName("cookie")]
            public string Cookie { get => cookie; set => cookie = value ?? ""; }
        }
        
        private string getTokenUrl = "";
        private string punchUrl = "";
        private string postReferer = "";
        private string postUserAgent = "";
        private UserSetting[] users = Array.Empty<UserSetting>();

        [JsonPropertyName("getTokenUrl")]
        public string GetTokenUrl { get => getTokenUrl; set => getTokenUrl = value ?? ""; }

        [JsonPropertyName("punchUrl")]
        public string PunchUrl { get => punchUrl; set => punchUrl = value ?? ""; }
        
        [JsonPropertyName("referer")]
        public string PostReferer { get => postReferer; set => postReferer = value ?? ""; }

        [JsonPropertyName("user-agent")]
        public string PostUserAgent { get => postUserAgent; set => postUserAgent = value ?? ""; }
        
        [JsonPropertyName("users")]
        public UserSetting[] Users { get => users; set => users = value ?? Array.Empty<UserSetting>(); }
    }
}