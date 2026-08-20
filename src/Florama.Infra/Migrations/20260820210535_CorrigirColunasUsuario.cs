using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Florama.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirColunasUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH('dbo.USUARIO', 'ATIVO') IS NULL
                    ALTER TABLE [USUARIO] ADD [ATIVO] bit NOT NULL DEFAULT CAST(0 AS bit);

                IF COL_LENGTH('dbo.USUARIO', 'EMAILCONFIRMADO') IS NULL
                    ALTER TABLE [USUARIO] ADD [EMAILCONFIRMADO] bit NOT NULL DEFAULT CAST(0 AS bit);

                IF COL_LENGTH('dbo.USUARIO', 'REFRESHTOKEN') IS NULL
                    ALTER TABLE [USUARIO] ADD [REFRESHTOKEN] varchar(500) NULL;

                IF COL_LENGTH('dbo.USUARIO', 'REFRESHTOKENEXPIRACAO') IS NULL
                    ALTER TABLE [USUARIO] ADD [REFRESHTOKENEXPIRACAO] datetime2 NULL;

                IF COL_LENGTH('dbo.USUARIO', 'SENHAHASH') IS NULL
                    ALTER TABLE [USUARIO] ADD [SENHAHASH] varchar(255) NOT NULL DEFAULT '';

                IF COL_LENGTH('dbo.USUARIO', 'ULTIMOLOGIN') IS NULL
                    ALTER TABLE [USUARIO] ADD [ULTIMOLOGIN] datetime2 NULL;

                IF EXISTS (SELECT 1 FROM sys.columns
                           WHERE object_id = OBJECT_ID('dbo.USUARIO')
                             AND name = 'ISADMIN'
                             AND system_type_id = 56)
                    ALTER TABLE [USUARIO] ALTER COLUMN [ISADMIN] bit NOT NULL;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH('dbo.USUARIO', 'ULTIMOLOGIN') IS NOT NULL
                    ALTER TABLE [USUARIO] DROP COLUMN [ULTIMOLOGIN];

                IF COL_LENGTH('dbo.USUARIO', 'SENHAHASH') IS NOT NULL
                    ALTER TABLE [USUARIO] DROP COLUMN [SENHAHASH];

                IF COL_LENGTH('dbo.USUARIO', 'REFRESHTOKENEXPIRACAO') IS NOT NULL
                    ALTER TABLE [USUARIO] DROP COLUMN [REFRESHTOKENEXPIRACAO];

                IF COL_LENGTH('dbo.USUARIO', 'REFRESHTOKEN') IS NOT NULL
                    ALTER TABLE [USUARIO] DROP COLUMN [REFRESHTOKEN];

                IF COL_LENGTH('dbo.USUARIO', 'EMAILCONFIRMADO') IS NOT NULL
                    ALTER TABLE [USUARIO] DROP COLUMN [EMAILCONFIRMADO];

                IF COL_LENGTH('dbo.USUARIO', 'ATIVO') IS NOT NULL
                    ALTER TABLE [USUARIO] DROP COLUMN [ATIVO];

                IF EXISTS (SELECT 1 FROM sys.columns
                           WHERE object_id = OBJECT_ID('dbo.USUARIO')
                             AND name = 'ISADMIN'
                             AND system_type_id = 104)
                    ALTER TABLE [USUARIO] ALTER COLUMN [ISADMIN] int NOT NULL;
            ");
        }
    }
}
