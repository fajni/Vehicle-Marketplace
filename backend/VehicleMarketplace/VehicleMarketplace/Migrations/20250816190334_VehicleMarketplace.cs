using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VehicleMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class VehicleMarketplace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "vin",
                table: "motorcycles",
                type: "varchar(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "vin",
                table: "cars",
                type: "varchar(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.InsertData(
                table: "makes",
                columns: new[] { "make_id", "make_name" },
                values: new object[,]
                {
                    { 1, "BMW" },
                    { 2, "Audi" },
                    { 3, "Dodge" },
                    { 4, "Alfa Romeo" },
                    { 5, "Nissan" },
                    { 6, "Dacia" },
                    { 7, "Bentley" },
                    { 8, "Chevrolet" },
                    { 9, "Ford" },
                    { 10, "Honda" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_makes_make_name",
                table: "makes",
                column: "make_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_makes_make_name",
                table: "makes");

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "makes",
                keyColumn: "make_id",
                keyValue: 10);

            migrationBuilder.AlterColumn<string>(
                name: "vin",
                table: "motorcycles",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(17)",
                oldMaxLength: 17);

            migrationBuilder.AlterColumn<string>(
                name: "vin",
                table: "cars",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(17)",
                oldMaxLength: 17);
        }
    }
}
