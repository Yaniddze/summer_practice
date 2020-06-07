namespace AspGateway.Entities
{
    public class Urls
    {
        public class IdentityRoutes
        {
            public static string Login = "/api/identity/login";
            public static string Register = "/api/identity/register";
            public static string Refresh = "/api/identity/refresh";
            public static string Activate = "/api/identity/activate";
        }

        public class StreamingRoutes
        {
            public static string Music = "/api/streaming/music";
        }

        public string Identity { get; set; }
        public string Streaming { get; set; }
    }
}