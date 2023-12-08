using Exercise.DataBase;
using Exercise.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace WebApi.TokenConfig
{
    public class Token
    {
        private readonly DBContext _dBContext;
        private readonly IConfiguration _configuration;
        public Token (IConfiguration configuration, DBContext dBContext)
        {
            _configuration = configuration;
            _dBContext = dBContext;
        }
        public string CreateToken(Admin admin)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token256").Value!)); 
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime now = DateTime.Now; 
            int expirationMinutes = 30; // Đặt thời gian hết hạn là 30 phút
            DateTime expiration = now.AddMinutes(expirationMinutes); 

            var token = new JwtSecurityToken(claims: claims, expires: expiration,
                signingCredentials: cred, issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"]);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Token256"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                return principal;
            }
            catch (Exception)
            {
                // Trả về null nếu token không hợp lệ
                return null;
            }
        }
    

    }
}
