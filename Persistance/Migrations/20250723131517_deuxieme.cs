using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistance.Migrations
{
    public partial class Deuxieme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ajoute une nouvelle colonne UUID avec valeur générée par gen_random_uuid()
            migrationBuilder.AddColumn<Guid>(
                name: "Id_temp",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            // Supprime la clé primaire actuelle (le nom correct est Products_pkey)
            migrationBuilder.DropPrimaryKey(
                name: "Products_pkey",
                table: "Products");

            // Supprime l'ancienne colonne Id
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            // Renomme la colonne temporaire Id_temp en Id
            migrationBuilder.RenameColumn(
                name: "Id_temp",
                table: "Products",
                newName: "Id");

            // Ajoute la clé primaire sur la nouvelle colonne Id (UUID)
            migrationBuilder.AddPrimaryKey(
                name: "Products_pkey",
                table: "Products",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Ajoute la colonne Id temporaire de type integer avec identité
            migrationBuilder.AddColumn<int>(
                name: "Id_temp",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy",
                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            // Supprime la clé primaire actuelle (sur Id UUID)
            migrationBuilder.DropPrimaryKey(
                name: "Products_pkey",
                table: "Products");

            // Supprime la colonne Id (UUID)
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            // Renomme la colonne temporaire Id_temp en Id
            migrationBuilder.RenameColumn(
                name: "Id_temp",
                table: "Products",
                newName: "Id");

            // Ajoute la clé primaire sur la colonne Id (integer)
            migrationBuilder.AddPrimaryKey(
                name: "Products_pkey",
                table: "Products",
                column: "Id");
        }
    }
}
