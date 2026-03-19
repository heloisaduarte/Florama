using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Florama.Data.Migrations
{
    /// <inheritdoc />
    public partial class Produto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    VALOR = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false),
                    CATEGORIAID = table.Column<long>(type: "bigint", nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    USUARIOCRIACAO = table.Column<long>(type: "bigint", nullable: false),
                    DATAALTERACAO = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    USUARIOALTERACAO = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTO_CATEGORIA_CATEGORIAID",
                        column: x => x.CATEGORIAID,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_CATEGORIAID",
                table: "PRODUTO",
                column: "CATEGORIAID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
