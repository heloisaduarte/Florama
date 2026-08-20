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
        public static async Task InitializeDatabaseAsync(IServiceProvider services, IConfiguration configuration)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FloramaDbContext>();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            await db.Database.MigrateAsync();

            await SeedAdminAsync(db, hasher, configuration);
            await SeedDemoDataAsync(db, configuration);
        }

        private static async Task SeedAdminAsync(FloramaDbContext db, IPasswordHasher hasher, IConfiguration configuration)
        {
            var email = configuration["Admin:Email"];
            var senha = configuration["Admin:Senha"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                return;

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

        private static async Task SeedDemoDataAsync(FloramaDbContext db, IConfiguration configuration)
        {
            var habilitado = !string.Equals(configuration["SeedDemoData"], "false", StringComparison.OrdinalIgnoreCase);

            if (!habilitado)
                return;

            var existeCategoria = await db.Set<Categoria>().AnyAsync();

            if (existeCategoria)
                return;

            var velas = new Categoria("Velas Aromáticas", "Velas artesanais para aromatizar e decorar o ambiente");
            var sabonetes = new Categoria("Sabonetes Artesanais", "Sabonetes naturais feitos à mão");
            var aromatizadores = new Categoria("Aromatizadores", "Aromatizadores de ambiente em spray e difusores");

            db.AddRange(velas, sabonetes, aromatizadores);

            AdicionarProduto(db, "Vela de Lavanda", 25.90m, velas, 12,
                "https://placehold.co/400x400/png?text=Vela+de+Lavanda");

            AdicionarProduto(db, "Vela de Eucalipto", 27.50m, velas, 8,
                "https://placehold.co/400x400/png?text=Vela+de+Eucalipto");

            AdicionarProduto(db, "Vela de Baunilha", 24.90m, velas, 10,
                "https://placehold.co/400x400/png?text=Vela+de+Baunilha",
                "https://placehold.co/400x400/png?text=Vela+de+Baunilha+2");

            AdicionarProduto(db, "Sabonete de Mel e Aveia", 12.90m, sabonetes, 30,
                "https://placehold.co/400x400/png?text=Sabonete+de+Mel+e+Aveia");

            AdicionarProduto(db, "Sabonete de Argila e Hortelã", 13.50m, sabonetes, 25,
                "https://placehold.co/400x400/png?text=Sabonete+de+Argila");

            AdicionarProduto(db, "Aromatizador de Lavanda", 19.90m, aromatizadores, 15,
                "https://placehold.co/400x400/png?text=Aromatizador+de+Lavanda");

            AdicionarProduto(db, "Aromatizador de Capim-Limão", 19.90m, aromatizadores, 20,
                "https://placehold.co/400x400/png?text=Aromatizador+de+Capim-Limao");

            await db.SaveChangesAsync();
        }

        private static void AdicionarProduto(
            FloramaDbContext db,
            string nome,
            decimal valor,
            Categoria categoria,
            int quantidadeEstoque,
            params string[] fotos)
        {
            var produto = new Produto();
            produto.DefinirNome(nome);
            produto.DefinirValor(valor);
            produto.DefinirCategoria(categoria);

            foreach (var url in fotos)
            {
                produto.AdicionarFoto(url);
            }

            var estoque = new Estoque();
            estoque.DefinirProduto(produto);
            estoque.Adicionar(quantidadeEstoque);

            db.Add(produto);
            db.Add(estoque);
        }
    }
}