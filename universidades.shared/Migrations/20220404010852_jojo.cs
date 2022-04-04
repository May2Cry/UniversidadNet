using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace universidades.shared.Migrations
{
    public partial class jojo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Universidades",
                columns: table => new
                {
                    IdUniversidade = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domains = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlphaTwoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebPages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    favorito = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universidades", x => x.IdUniversidade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Universidades");
        }
    }
}
