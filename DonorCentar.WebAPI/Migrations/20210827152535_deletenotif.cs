using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorCentar.WebAPI.Migrations
{
    public partial class deletenotif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifikacija");

            migrationBuilder.DropTable(
                name: "TipNotifikacije");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipNotifikacije",
                columns: table => new
                {
                    TipNotifikacijeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notifikacija = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipNotifikacije", x => x.TipNotifikacijeId);
                });

            migrationBuilder.CreateTable(
                name: "Notifikacija",
                columns: table => new
                {
                    NotifikacijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonacijaId = table.Column<int>(type: "int", nullable: true),
                    OdKorisnikId = table.Column<int>(type: "int", nullable: false),
                    TipKorisnikaId = table.Column<int>(type: "int", nullable: false),
                    TipNotifikacijeId = table.Column<int>(type: "int", nullable: false),
                    Vrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ZaKorisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacija", x => x.NotifikacijaId);
                    table.ForeignKey(
                        name: "FK_Notifikacija_Donacija_DonacijaId",
                        column: x => x.DonacijaId,
                        principalTable: "Donacija",
                        principalColumn: "DonacijaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifikacija_Korisnik_OdKorisnikId",
                        column: x => x.OdKorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifikacija_TipNotifikacije_TipNotifikacijeId",
                        column: x => x.TipNotifikacijeId,
                        principalTable: "TipNotifikacije",
                        principalColumn: "TipNotifikacijeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifikacija_Korisnik_ZaKorisnikId",
                        column: x => x.ZaKorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacija_DonacijaId",
                table: "Notifikacija",
                column: "DonacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacija_OdKorisnikId",
                table: "Notifikacija",
                column: "OdKorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacija_TipNotifikacijeId",
                table: "Notifikacija",
                column: "TipNotifikacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacija_ZaKorisnikId",
                table: "Notifikacija",
                column: "ZaKorisnikId");
        }
    }
}
