using System.Text;

namespace TestApi.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public byte[] SecretInBytes => Encoding.ASCII.GetBytes(Secret);
    }
}