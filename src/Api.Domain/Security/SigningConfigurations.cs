using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Security;

public class SigningConfigurations
{
    public static SecurityKey Key { get; set; }
    public SigningCredentials SigningCredentials { get; set; }

    public SigningConfigurations()
    {
        
        /*using (var provider = new RSACryptoServiceProvider(512))
        {
            Key = new RsaSecurityKey(provider.ExportParameters(true));
        }
        SigningCredentials = new SigningCredentials(Key,SecurityAlgorithms.RsaSha512Signature);*/
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1b54eaca-8c0c-4532-a03e-856c6676dd93-123"));
        SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
    }
}