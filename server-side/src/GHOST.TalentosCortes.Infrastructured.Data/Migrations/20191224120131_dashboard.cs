using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GHOST.TalentosCortes.Infrastructured.Data.Migrations
{
    public partial class dashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dashboards",
                columns: table => new
                {
              
                    YearDate = table.Column<string>(type: "varchar(5)", nullable: true),
                    Janeiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fevereiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abril = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Junho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Julho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agosto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Setembro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outubro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Novembro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dezembro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dashboards");
        }
    }
}
