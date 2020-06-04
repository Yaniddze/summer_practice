using System.Text;

namespace AspGateway.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public byte[] SecretInBytes => Encoding.ASCII.GetBytes(Secret);
    }
}