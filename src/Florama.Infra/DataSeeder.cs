using Florama.Data;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Florama.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider services, IConfiguration configuration)
        {
            var email = configuration["Admin:Email"];
            var senha = configuration["Admin:Senha"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                return;

            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FloramaDbContext>();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            var existe = await db.Set<Usuario>().AnyAsync(u => u.Email.Endereco == email);

            if (existe)
                return;

            var administrador = new Usuario(
                nome: "Administrador",
                email: email,
                cpf: "52998224725",
                dataNascimento: new DateOnly(1990, 1, 1),
                telefone: "(11) 99999-9999",
                isAdmin: true,
                senhaHash: hasher.Hash(senha));

            administrador.ConfirmarEmail();

            db.Add(administrador);
            await db.SaveChangesAsync();
        }
    }
}