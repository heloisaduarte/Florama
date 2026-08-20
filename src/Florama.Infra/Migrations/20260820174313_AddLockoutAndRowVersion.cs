using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Florama.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLockoutAndRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BLOQUEIOATE",
                table: "USUARIO",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TENTATIVASLOGINFALHAS",
                table: "USUARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "ROWVERSION",
                table: "ESTOQUE",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BLOQUEIOATE",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "TENTATIVASLOGINFALHAS",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "ROWVERSION",
                table: "ESTOQUE");
        }
    }
}
