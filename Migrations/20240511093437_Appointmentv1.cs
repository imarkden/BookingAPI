using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaintJohnDentalClinicApi.Migrations
{
    /// <inheritdoc />
    public partial class Appointmentv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Books_bookId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_doctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Serives_serviceId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "serviceId",
                table: "Appointments",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "doctorId",
                table: "Appointments",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "bookId",
                table: "Appointments",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_serviceId",
                table: "Appointments",
                newName: "IX_Appointments_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_doctorId",
                table: "Appointments",
                newName: "IX_Appointments_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_bookId",
                table: "Appointments",
                newName: "IX_Appointments_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Books_BookId",
                table: "Appointments",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Serives_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Serives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Books_BookId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Serives_ServiceId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Appointments",
                newName: "serviceId");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Appointments",
                newName: "doctorId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Appointments",
                newName: "bookId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                newName: "IX_Appointments_serviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                newName: "IX_Appointments_doctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_BookId",
                table: "Appointments",
                newName: "IX_Appointments_bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Books_bookId",
                table: "Appointments",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_doctorId",
                table: "Appointments",
                column: "doctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Serives_serviceId",
                table: "Appointments",
                column: "serviceId",
                principalTable: "Serives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
