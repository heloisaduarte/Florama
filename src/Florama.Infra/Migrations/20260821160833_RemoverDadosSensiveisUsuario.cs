using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Florama.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoverDadosSensiveisUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "DATANASCIMENTO",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "TELEFONE",
                table: "USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "USUARIO",
                type: "varchar(11)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DATANASCIMENTO",
                table: "USUARIO",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "TELEFONE",
                table: "USUARIO",
                type: "varchar(11)",
                nullable: false,
                defaultValue: "");
        }
    }
}
