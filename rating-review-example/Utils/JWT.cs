using Microsoft.IdentityModel.Tokens;
using rating_review_example.Models;
using rating_review_example.Repositories;
using rating_review_example.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace rating_review_example.Utils
{
    
    public class JWT
    {
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public LoginReponse Encode(PassCode passCode, int expired = 1)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Id", passCode.Id.ToString()),
                    new Claim("ServiceId", passCode.ServiceId.ToString())
                }),

                Expires = now.AddHours(Convert.ToInt32(expired)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            var refreshToken = generateRefreshToken();
            return new LoginReponse { 
                Token = token,
                RefreshToken = refreshToken,
            };
        }

        public int Verify(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(Secret));
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
                var payload = new JwtSecurityTokenHandler().ValidateToken(token,
                    tokenValidationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var id = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
                return id;
            } catch (Exception ex)
            {
                throw new Exception("Phiên đăng nhập không hợp lệ");
            }
            
        }

        private string generateRefreshToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}