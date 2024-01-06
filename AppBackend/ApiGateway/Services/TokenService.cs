
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiGateway.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Services {
    public class TokenService {

        public string GerarToken(Usuario usuario) {

            var key = Encoding.ASCII.GetBytes("ivannaivannaivannaivannaivannaivannaivannaivannaivannaivannaivannaivanna");
            
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new() {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                ),
                Subject = new ClaimsIdentity(new[] {
                        new Claim("Login", usuario.Login),
                        new Claim("Coren", usuario.Coren),
                        new Claim("Grupo", usuario.Grupo.ToString())
                    }
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string strToken = tokenHandler.WriteToken(token);

            return strToken;
        }
    }
}