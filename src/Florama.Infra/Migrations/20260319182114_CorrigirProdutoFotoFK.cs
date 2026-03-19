using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Florama.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirProdutoFotoFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTOFOTO_PRODUTO_ProdutoId1",
                table: "PRODUTOFOTO");

            migrationBuilder.DropIndex(
                name: "IX_PRODUTOFOTO_ProdutoId1",
                table: "PRODUTOFOTO");

            migrationBuilder.DropColumn(
                name: "ProdutoId1",
                table: "PRODUTOFOTO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProdutoId1",
                table: "PRODUTOFOTO",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOFOTO_ProdutoId1",
                table: "PRODUTOFOTO",
                column: "ProdutoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTOFOTO_PRODUTO_ProdutoId1",
                table: "PRODUTOFOTO",
                column: "ProdutoId1",
                principalTable: "PRODUTO",
                principalColumn: "ID");
        }
    }
}
