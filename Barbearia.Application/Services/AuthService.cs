using Barbearia.Application.Interfaces;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Barbearia.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        public string Login(TenantId tenantId, string email, string senha)
        {
            var user = _usuarioRepository.ObterPorId(tenantId, email);
            if (user == null || user.SenhaHash != senha)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("tenant_id", user.TenantId.Value.ToString()),
                new Claim(ClaimTypes.Role, user.Perfil)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
