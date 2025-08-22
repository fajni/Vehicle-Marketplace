using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_cars_image_vin",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_motorcycles_image_vin",
                table: "images");

            migrationBuilder.RenameColumn(
                name: "image_vin",
                table: "images",
                newName: "image_motorcycle_vin");

            migrationBuilder.RenameIndex(
                name: "IX_images_image_vin",
                table: "images",
                newName: "IX_images_image_motorcycle_vin");

            migrationBuilder.AddColumn<string>(
                name: "image_car_vin",
                table: "images",
                type: "varchar(17)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_image_car_vin",
                table: "images",
                column: "image_car_vin");

            migrationBuilder.AddForeignKey(
                name: "FK_images_cars_image_car_vin",
                table: "images",
                column: "image_car_vin",
                principalTable: "cars",
                principalColumn: "vin",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_images_motorcycles_image_motorcycle_vin",
                table: "images",
                column: "image_motorcycle_vin",
                principalTable: "motorcycles",
                principalColumn: "vin",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_cars_image_car_vin",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_motorcycles_image_motorcycle_vin",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_image_car_vin",
                table: "images");

            migrationBuilder.DropColumn(
                name: "image_car_vin",
                table: "images");

            migrationBuilder.RenameColumn(
                name: "image_motorcycle_vin",
                table: "images",
                newName: "image_vin");

            migrationBuilder.RenameIndex(
                name: "IX_images_image_motorcycle_vin",
                table: "images",
                newName: "IX_images_image_vin");

            migrationBuilder.AddForeignKey(
                name: "FK_images_cars_image_vin",
                table: "images",
                column: "image_vin",
                principalTable: "cars",
                principalColumn: "vin",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_images_motorcycles_image_vin",
                table: "images",
                column: "image_vin",
                principalTable: "motorcycles",
                principalColumn: "vin",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
