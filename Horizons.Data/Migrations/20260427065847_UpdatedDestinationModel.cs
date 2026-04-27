using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizons.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDestinationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationId1",
                table: "UsersDestinations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Continent",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Destinations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Destinations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Destinations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TravelDistance",
                table: "Destinations",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "639842ca-e938-4fbc-ae53-2672aa7744d7", "AQAAAAIAAYagAAAAEEDclj+NdzkBSSiv1Z2sFipEBtj+W3SjIkBAQcsYEjB03xgqmXIdXSE0cyK+qpC2dQ==", "275da879-7f90-491d-b2a4-49bc0e6b7e35" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Continent", "Country", "Latitude", "Longitude", "Rating", "TravelDistance" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Continent", "Country", "Latitude", "Longitude", "Rating", "TravelDistance" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Continent", "Country", "Latitude", "Longitude", "Rating", "TravelDistance" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Continent", "Country", "Latitude", "Longitude", "Rating", "TravelDistance" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Continent", "Country", "Latitude", "Longitude", "Rating", "TravelDistance" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_UsersDestinations_DestinationId1",
                table: "UsersDestinations",
                column: "DestinationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDestinations_Destinations_DestinationId1",
                table: "UsersDestinations",
                column: "DestinationId1",
                principalTable: "Destinations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersDestinations_Destinations_DestinationId1",
                table: "UsersDestinations");

            migrationBuilder.DropIndex(
                name: "IX_UsersDestinations_DestinationId1",
                table: "UsersDestinations");

            migrationBuilder.DropColumn(
                name: "DestinationId1",
                table: "UsersDestinations");

            migrationBuilder.DropColumn(
                name: "Continent",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "TravelDistance",
                table: "Destinations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "127beed2-f804-4bcd-958b-e0572f8accd4", "AQAAAAIAAYagAAAAEK8YyzpRgnLPLZcfy/+IGgACkVUFQ/1nlSytO5WBZnmXv7qzGyEdLyY6QiBjDeB64A==", "3d6c519f-ac62-4d8a-958a-f9b7d85a9d10" });
        }
    }
}
