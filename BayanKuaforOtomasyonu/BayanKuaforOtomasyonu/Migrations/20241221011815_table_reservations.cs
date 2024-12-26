using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BayanKuaforOtomasyonu.Migrations
{
    /// <inheritdoc />
    public partial class table_reservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResId = table.Column<int>(type: "int", nullable: false),
                    AppUserEmploymentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_AppUserEmployement_AppUserEmploymentId",
                        column: x => x.AppUserEmploymentId,
                        principalTable: "AppUserEmployement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_Reservations_ResId",
                        column: x => x.ResId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_AppUserEmploymentId",
                table: "ReservationDetails",
                column: "AppUserEmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_ResId",
                table: "ReservationDetails",
                column: "ResId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AppUserId",
                table: "Reservations",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationDetails");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
