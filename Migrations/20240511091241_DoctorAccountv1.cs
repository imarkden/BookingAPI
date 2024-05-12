using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaintJohnDentalClinicApi.Migrations
{
    /// <inheritdoc />
    public partial class DoctorAccountv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAccounts_Doctors_doctorId",
                table: "DoctorAccounts");

            migrationBuilder.RenameColumn(
                name: "doctorId",
                table: "DoctorAccounts",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorAccounts_doctorId",
                table: "DoctorAccounts",
                newName: "IX_DoctorAccounts_DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAccounts_Doctors_DoctorId",
                table: "DoctorAccounts",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAccounts_Doctors_DoctorId",
                table: "DoctorAccounts");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "DoctorAccounts",
                newName: "doctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorAccounts_DoctorId",
                table: "DoctorAccounts",
                newName: "IX_DoctorAccounts_doctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAccounts_Doctors_doctorId",
                table: "DoctorAccounts",
                column: "doctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
