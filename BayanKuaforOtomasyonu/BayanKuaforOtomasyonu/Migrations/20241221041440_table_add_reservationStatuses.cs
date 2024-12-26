using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BayanKuaforOtomasyonu.Migrations
{
    /// <inheritdoc />
    public partial class table_add_reservationStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reservationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResId = table.Column<int>(type: "int", nullable: false),
                    isStatus = table.Column<bool>(type: "bit", nullable: true),
                    isUserEdit = table.Column<bool>(type: "bit", nullable: false),
                    isManagerEdit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservationStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservationStatuses_Reservations_ResId",
                        column: x => x.ResId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservationStatuses_ResId",
                table: "reservationStatuses",
                column: "ResId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservationStatuses");
        }
    }
}
