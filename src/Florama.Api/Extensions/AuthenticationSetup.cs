using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Florama.Api.Extensions
{
    public static class AuthenticationSetup
    {
        private const string ChavePadraoDesenvolvimento = "uma-chave-com-pelo-menos-32-caracteres";

        public static IServiceCollection AddAuthenticationSetup(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var secretKey = configuration["Jwt:SecretKey"];
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException(
                    "Jwt:SecretKey não está configurada. Defina via variável de ambiente (Jwt__SecretKey) ou user-secrets.");
            }

            if (Encoding.UTF8.GetByteCount(secretKey) < 32)
            {
                throw new InvalidOperationException("Jwt:SecretKey deve ter pelo menos 32 bytes.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static void ValidarConfiguracaoJwt(IConfiguration configuration, bool isDevelopment)
        {
            if (isDevelopment)
                return;

            var secretKey = configuration["Jwt:SecretKey"];

            if (string.IsNullOrWhiteSpace(secretKey) || secretKey == ChavePadraoDesenvolvimento)
            {
                throw new InvalidOperationException(
                    "Jwt:SecretKey não configurada para produção. Use variável de ambiente (Jwt__SecretKey) ou user-secrets.");
            }
        }
    }
}