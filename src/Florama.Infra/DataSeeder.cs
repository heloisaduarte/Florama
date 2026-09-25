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

            // Azure SQL Serverless pode estar pausado (auto-pause).
            // Ao receber uma conexão, ele leva de 30 a 60 segundos para acordar (erro 40925 ou 40613).
            const int maxTentativas = 6;
            var delayTentativa = TimeSpan.FromSeconds(10);

            for (var tentativa = 1; tentativa <= maxTentativas; tentativa++)
            {
                try
                {
                    await db.Database.MigrateAsync();
                    await SeedAdminAsync(db, hasher, configuration);
                    await SeedDemoDataAsync(db, configuration);
                    break;
                }
                catch (Microsoft.Data.SqlClient.SqlException ex) when (tentativa < maxTentativas && (ex.Number == 40925 || ex.Number == 40613 || ex.Number == 40501 || ex.Number == 40197))
                {
                    Console.WriteLine($"[Azure SQL] Banco de dados inicializando/acordando (erro {ex.Number}). Aguardando retomada... tentativa {tentativa}/{maxTentativas}");
                    await Task.Delay(delayTentativa);
                }
            }
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

            AdicionarProduto(db, "Vela de Lavanda", "Vela aromática artesanal com notas florais relaxantes de lavanda", 25.90m, velas, 12,
                "https://placehold.co/400x400/png?text=Vela+de+Lavanda");

            AdicionarProduto(db, "Vela de Eucalipto", "Vela aromática artesanal com aroma refrescante e purificador de eucalipto", 27.50m, velas, 8,
                "https://placehold.co/400x400/png?text=Vela+de+Eucalipto");

            AdicionarProduto(db, "Vela de Baunilha", "Vela aromática artesanal com toque doce e acolhedor de baunilha", 24.90m, velas, 10,
                "https://placehold.co/400x400/png?text=Vela+de+Baunilha",
                "https://placehold.co/400x400/png?text=Vela+de+Baunilha+2");

            AdicionarProduto(db, "Sabonete de Mel e Aveia", "Sabonete artesanal hidratante e esfoliante com mel natural e aveia", 12.90m, sabonetes, 30,
                "https://placehold.co/400x400/png?text=Sabonete+de+Mel+e+Aveia");

            AdicionarProduto(db, "Sabonete de Argila e Hortelã", "Sabonete artesanal purificante com argila verde e óleo essencial de hortelã", 13.50m, sabonetes, 25,
                "https://placehold.co/400x400/png?text=Sabonete+de+Argila");

            AdicionarProduto(db, "Aromatizador de Lavanda", "Difusor de ambientes com fragrância suave e tranquilizante de lavanda", 19.90m, aromatizadores, 15,
                "https://placehold.co/400x400/png?text=Aromatizador+de+Lavanda");

            AdicionarProduto(db, "Aromatizador de Capim-Limão", "Aromatizador de ambiente em spray com aroma cítrico e revigorante de capim-limão", 19.90m, aromatizadores, 20,
                "https://placehold.co/400x400/png?text=Aromatizador+de+Capim-Limao");

            await db.SaveChangesAsync();
        }

        private static void AdicionarProduto(
            FloramaDbContext db,
            string nome,
            string descricao,
            decimal valor,
            Categoria categoria,
            int quantidadeEstoque,
            params string[] fotos)
        {
            var produto = new Produto();
            produto.DefinirNome(nome);
            produto.DefinirDescricao(descricao);
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