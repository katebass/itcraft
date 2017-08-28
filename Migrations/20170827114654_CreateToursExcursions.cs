using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Travels.Migrations
{
    public partial class CreateToursExcursions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tours_Excursions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Excursion_SightsID = table.Column<int>(type: "int", nullable: false),
                    ToursID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours_Excursions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tours_Excursions_Excursion_Sights_Excursion_SightsID",
                        column: x => x.Excursion_SightsID,
                        principalTable: "Excursion_Sights",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tours_Excursions_Tours_ToursID",
                        column: x => x.ToursID,
                        principalTable: "Tours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tours_Excursions_Excursion_SightsID",
                table: "Tours_Excursions",
                column: "Excursion_SightsID");
            
            migrationBuilder.CreateIndex(
                name: "IX_Tours_Excursions_ToursID",
                table: "Tours_Excursions",
                column: "ToursID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tours_Excursions");
        }
    }
}
