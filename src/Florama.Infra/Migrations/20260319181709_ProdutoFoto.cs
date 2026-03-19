using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Florama.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProdutoFoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUTOFOTO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUTOID = table.Column<long>(type: "bigint", nullable: false),
                    URL = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    ORDEM = table.Column<int>(type: "INT", nullable: false),
                    ProdutoId1 = table.Column<long>(type: "bigint", nullable: true),
                    DATACRIACAO = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    USUARIOCRIACAO = table.Column<long>(type: "bigint", nullable: false),
                    DATAALTERACAO = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    USUARIOALTERACAO = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTOFOTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTOFOTO_PRODUTO_PRODUTOID",
                        column: x => x.PRODUTOID,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUTOFOTO_PRODUTO_ProdutoId1",
                        column: x => x.ProdutoId1,
                        principalTable: "PRODUTO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOFOTO_PRODUTOID",
                table: "PRODUTOFOTO",
                column: "PRODUTOID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOFOTO_ProdutoId1",
                table: "PRODUTOFOTO",
                column: "ProdutoId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUTOFOTO");
        }
    }
}
