using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorCentar.Web.Migrations
{
    public partial class admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Obavijest_AdminId",
                table: "Obavijest",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijest_Administrator_AdminId",
                table: "Obavijest",
                column: "AdminId",
                principalTable: "Administrator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obavijest_Administrator_AdminId",
                table: "Obavijest");

            migrationBuilder.DropIndex(
                name: "IX_Obavijest_AdminId",
                table: "Obavijest");
        }
    }
}
