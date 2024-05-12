using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaintJohnDentalClinicApi.Migrations
{
    /// <inheritdoc />
    public partial class Appointmentv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Serives_ServiceId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Serives",
                table: "Serives");

            migrationBuilder.RenameTable(
                name: "Serives",
                newName: "Services");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Serives");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Serives",
                table: "Serives",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Serives_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Serives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
