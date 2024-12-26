using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BayanKuaforOtomasyonu.Migrations
{
    /// <inheritdoc />
    public partial class connecttablesedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserEmployement_AspNetUsers_AppUserId",
                table: "AppUserEmployement");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserEmployement_Employments_EmploymentId",
                table: "AppUserEmployement");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationDetails_AppUserEmployement_AppUserEmploymentId",
                table: "ReservationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationDetails_Reservations_ResId",
                table: "ReservationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_AppUserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_reservationStatuses_Reservations_ResId",
                table: "reservationStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserEmployement_AspNetUsers_AppUserId",
                table: "AppUserEmployement",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserEmployement_Employments_EmploymentId",
                table: "AppUserEmployement",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationDetails_AppUserEmployement_AppUserEmploymentId",
                table: "ReservationDetails",
                column: "AppUserEmploymentId",
                principalTable: "AppUserEmployement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationDetails_Reservations_ResId",
                table: "ReservationDetails",
                column: "ResId",
                principalTable: "Reservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_AppUserId",
                table: "Reservations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reservationStatuses_Reservations_ResId",
                table: "reservationStatuses",
                column: "ResId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserEmployement_AspNetUsers_AppUserId",
                table: "AppUserEmployement");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserEmployement_Employments_EmploymentId",
                table: "AppUserEmployement");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationDetails_AppUserEmployement_AppUserEmploymentId",
                table: "ReservationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationDetails_Reservations_ResId",
                table: "ReservationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_AppUserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_reservationStatuses_Reservations_ResId",
                table: "reservationStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserEmployement_AspNetUsers_AppUserId",
                table: "AppUserEmployement",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserEmployement_Employments_EmploymentId",
                table: "AppUserEmployement",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationDetails_AppUserEmployement_AppUserEmploymentId",
                table: "ReservationDetails",
                column: "AppUserEmploymentId",
                principalTable: "AppUserEmployement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationDetails_Reservations_ResId",
                table: "ReservationDetails",
                column: "ResId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_AppUserId",
                table: "Reservations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reservationStatuses_Reservations_ResId",
                table: "reservationStatuses",
                column: "ResId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
