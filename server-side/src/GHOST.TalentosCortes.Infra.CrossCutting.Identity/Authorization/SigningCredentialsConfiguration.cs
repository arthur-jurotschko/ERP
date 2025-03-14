using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Authorization
{
    public class SigningCredentialsConfiguration
    {
        const string SecretKey = "codefactory_formingparty@meuambienteToken";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
