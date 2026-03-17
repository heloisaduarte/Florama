using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Florama.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "VARCHAR(500)", maxLength: 500, nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    USUARIOCRIACAO = table.Column<long>(type: "bigint", nullable: false),
                    DATAALTERACAO = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    USUARIOALTERACAO = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CATEGORIA");
        }
    }
}
