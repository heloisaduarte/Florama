using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Florama.Data.Migrations
{
    /// <inheritdoc />
    public partial class Estoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTOQUE",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUTOID = table.Column<long>(type: "bigint", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "INT", nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    USUARIOCRIACAO = table.Column<long>(type: "bigint", nullable: false),
                    DATAALTERACAO = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    USUARIOALTERACAO = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTOQUE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ESTOQUE_PRODUTO_PRODUTOID",
                        column: x => x.PRODUTOID,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ESTOQUEMOVIMENTACAO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUTOID = table.Column<long>(type: "bigint", nullable: false),
                    TIPO = table.Column<int>(type: "INT", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "INT", nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    USUARIOCRIACAO = table.Column<long>(type: "bigint", nullable: false),
                    DATAALTERACAO = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    USUARIOALTERACAO = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTOQUEMOVIMENTACAO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ESTOQUEMOVIMENTACAO_PRODUTO_PRODUTOID",
                        column: x => x.PRODUTOID,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUE_PRODUTOID",
                table: "ESTOQUE",
                column: "PRODUTOID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUEMOVIMENTACAO_PRODUTOID",
                table: "ESTOQUEMOVIMENTACAO",
                column: "PRODUTOID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ESTOQUE");

            migrationBuilder.DropTable(
                name: "ESTOQUEMOVIMENTACAO");
        }
    }
}
