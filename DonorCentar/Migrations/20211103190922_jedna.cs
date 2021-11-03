using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorCentar.Web.Migrations
{
    public partial class jedna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dojam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VrstaDojma = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dojam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InformacijeTransporta",
                columns: table => new
                {
                    InformacijeTransportaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacijeTransporta", x => x.InformacijeTransportaId);
                });

            migrationBuilder.CreateTable(
                name: "Kanton",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanton", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicniPodaci",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    BrojTelefona = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ProfilnaSlika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicniPodaci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginPodaci",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Sifra = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginPodaci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obavijest",
                columns: table => new
                {
                    ObavijestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    Vrijeme = table.Column<DateTime>(nullable: false),
                    AdminId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijest", x => x.ObavijestId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "TipDonacije",
                columns: table => new
                {
                    TipDonacijeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipDonacije", x => x.TipDonacijeId);
                });

            migrationBuilder.CreateTable(
                name: "TipKorisnika",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipKorisnika", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrstaDonacije",
                columns: table => new
                {
                    VrstaDonacijeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrsta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaDonacije", x => x.VrstaDonacijeId);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    KantonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grad_Kanton_KantonId",
                        column: x => x.KantonId,
                        principalTable: "Kanton",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginPodaciId = table.Column<int>(nullable: false),
                    LicniPodaciId = table.Column<int>(nullable: false),
                    GradId = table.Column<int>(nullable: false),
                    TipKorisnikaId = table.Column<int>(nullable: false),
                    Izbrisan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korisnik_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Korisnik_LicniPodaci_LicniPodaciId",
                        column: x => x.LicniPodaciId,
                        principalTable: "LicniPodaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Korisnik_LoginPodaci_LoginPodaciId",
                        column: x => x.LoginPodaciId,
                        principalTable: "LoginPodaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Korisnik_TipKorisnika_TipKorisnikaId",
                        column: x => x.TipKorisnikaId,
                        principalTable: "TipKorisnika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrator_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donacija",
                columns: table => new
                {
                    DonacijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipDonacijeId = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: false),
                    Kolicina = table.Column<decimal>(nullable: false),
                    JedinicaMjere = table.Column<int>(nullable: false),
                    PrimalacId = table.Column<int>(nullable: true),
                    DonorId = table.Column<int>(nullable: true),
                    TransportId = table.Column<int>(nullable: true),
                    VrstaDonacijeId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    InformacijeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donacija", x => x.DonacijaId);
                    table.ForeignKey(
                        name: "FK_Donacija_Korisnik_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donacija_InformacijeTransporta_InformacijeId",
                        column: x => x.InformacijeId,
                        principalTable: "InformacijeTransporta",
                        principalColumn: "InformacijeTransportaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donacija_Korisnik_PrimalacId",
                        column: x => x.PrimalacId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donacija_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donacija_TipDonacije_TipDonacijeId",
                        column: x => x.TipDonacijeId,
                        principalTable: "TipDonacije",
                        principalColumn: "TipDonacijeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donacija_Korisnik_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donacija_VrstaDonacije_VrstaDonacijeId",
                        column: x => x.VrstaDonacijeId,
                        principalTable: "VrstaDonacije",
                        principalColumn: "VrstaDonacijeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    DatumRegistracije = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donor_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    DatumRegistracije = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partner_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Primalac",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    DatumRegistracije = table.Column<DateTime>(nullable: false),
                    Verifikovan = table.Column<bool>(nullable: false),
                    DokumentVerifikacije = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Primalac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Primalac_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DojamKorisnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DojamId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    DonacijaId = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DojamKorisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DojamKorisnik_Dojam_DojamId",
                        column: x => x.DojamId,
                        principalTable: "Dojam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DojamKorisnik_Donacija_DonacijaId",
                        column: x => x.DonacijaId,
                        principalTable: "Donacija",
                        principalColumn: "DonacijaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DojamKorisnik_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_KorisnikId",
                table: "Administrator",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_DojamKorisnik_DojamId",
                table: "DojamKorisnik",
                column: "DojamId");

            migrationBuilder.CreateIndex(
                name: "IX_DojamKorisnik_DonacijaId",
                table: "DojamKorisnik",
                column: "DonacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_DojamKorisnik_KorisnikId",
                table: "DojamKorisnik",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_DonorId",
                table: "Donacija",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_InformacijeId",
                table: "Donacija",
                column: "InformacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_PrimalacId",
                table: "Donacija",
                column: "PrimalacId");

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_StatusId",
                table: "Donacija",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_TipDonacijeId",
                table: "Donacija",
                column: "TipDonacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_TransportId",
                table: "Donacija",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_VrstaDonacijeId",
                table: "Donacija",
                column: "VrstaDonacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_Donor_KorisnikId",
                table: "Donor",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Grad_KantonId",
                table: "Grad",
                column: "KantonId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_GradId",
                table: "Korisnik",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_LicniPodaciId",
                table: "Korisnik",
                column: "LicniPodaciId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_LoginPodaciId",
                table: "Korisnik",
                column: "LoginPodaciId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_TipKorisnikaId",
                table: "Korisnik",
                column: "TipKorisnikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_KorisnikId",
                table: "Partner",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Primalac_KorisnikId",
                table: "Primalac",
                column: "KorisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "DojamKorisnik");

            migrationBuilder.DropTable(
                name: "Donor");

            migrationBuilder.DropTable(
                name: "Obavijest");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "Primalac");

            migrationBuilder.DropTable(
                name: "Dojam");

            migrationBuilder.DropTable(
                name: "Donacija");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "InformacijeTransporta");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "TipDonacije");

            migrationBuilder.DropTable(
                name: "VrstaDonacije");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "LicniPodaci");

            migrationBuilder.DropTable(
                name: "LoginPodaci");

            migrationBuilder.DropTable(
                name: "TipKorisnika");

            migrationBuilder.DropTable(
                name: "Kanton");
        }
    }
}
