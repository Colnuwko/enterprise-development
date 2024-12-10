using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingDetails.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Anewtypeforconnectingaroomandahotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_room_hotel_id",
                table: "room",
                column: "hotel_id");

            migrationBuilder.AddForeignKey(
                name: "FK_room_hotel_hotel_id",
                table: "room",
                column: "hotel_id",
                principalTable: "hotel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_room_hotel_hotel_id",
                table: "room");

            migrationBuilder.DropIndex(
                name: "IX_room_hotel_id",
                table: "room");
        }
    }
}
