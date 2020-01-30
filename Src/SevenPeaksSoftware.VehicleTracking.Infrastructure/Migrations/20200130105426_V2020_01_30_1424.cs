using Microsoft.EntityFrameworkCore.Migrations;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Migrations
{
    public partial class V2020_01_30_1424 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                maxLength: 320,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 320,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CreatedDateTime",
                table: "Vehicles",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CreatedDateTime_IsDeleted",
                table: "Vehicles",
                columns: new[] { "CreatedDateTime", "IsDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleRegistrationNumber_IsDeleted",
                table: "Vehicles",
                columns: new[] { "VehicleRegistrationNumber", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedDateTime_IsDeleted",
                table: "Users",
                columns: new[] { "CreatedDateTime", "IsDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username_IsDeleted",
                table: "Users",
                columns: new[] { "Username", "IsDeleted" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CreatedDateTime",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CreatedDateTime_IsDeleted",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleRegistrationNumber_IsDeleted",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Users_CreatedDateTime_IsDeleted",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username_IsDeleted",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                maxLength: 320,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 320,
                oldNullable: true,
                oldDefaultValue: "");
        }
    }
}
