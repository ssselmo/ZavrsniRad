using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorCentar.WebAPI.Migrations
{
    public partial class izbrisan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<bool>(
                name: "Izbrisan",
                table: "Korisnik",
                nullable: false,
                defaultValue: false);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "Izbrisan",
                table: "Korisnik");

        }
    }
}
