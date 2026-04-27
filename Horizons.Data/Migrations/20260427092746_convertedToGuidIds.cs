using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Horizons.Data.Migrations
{
    /// <inheritdoc />
    public partial class convertedToGuidIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersDestinations_Destinations_DestinationId1",
                table: "UsersDestinations");

            migrationBuilder.DropIndex(
                name: "IX_UsersDestinations_DestinationId1",
                table: "UsersDestinations");

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece");

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "DestinationId1",
                table: "UsersDestinations");

            migrationBuilder.RenameColumn(
                name: "PublishedOn",
                table: "Destinations",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<Guid>(
                name: "DestinationId",
                table: "UsersDestinations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Terrains",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Terrains",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Terrains",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Terrains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Terrains",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Terrains",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TerrainId",
                table: "Destinations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Destinations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Destinations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Destinations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Destinations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternateEmail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "AppUserId", "Continent", "Country", "CreatedAt", "DeletedAt", "DeletedBy", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "PublisherId", "Rating", "TerrainId", "TravelDistance", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0b295dfb-0e57-4155-a6c7-6d5e623d6568"), null, null, null, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "A mysterious cave located in the Rhodope Mountains.", "https://detskotobnr.binar.bg/wp-content/uploads/2017/11/Diavolsko_garlo_17.jpg", null, null, "Devil's Throat Cave", "7699db7d-964f-4782-8209-d76562e0fece", null, new Guid("84e1827c-2788-4cbb-9420-fd050dd6c66f"), null, null },
                    { new Guid("14026a35-d7e6-4fa1-88bd-ba5cafcefd0e"), null, null, null, new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "A beautiful sunny beach with golden sands and clear waters.", "https://example.com/images/sunny_beach.jpg", null, null, "Sunny Beach", "7699db7d-964f-4782-8209-d76562e0fece", null, new Guid("4c8d7f66-c207-42f3-9891-4e0b61cf893d"), null, null },
                    { new Guid("21255192-cd9f-4d6a-9bab-701ca83d1112"), null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "A range of mist-covered mountains perfect for hiking and adventure.", "https://example.com/images/misty_mountains.jpg", null, null, "Misty Mountains", "7699db7d-964f-4782-8209-d76562e0fece", null, new Guid("c05899b8-76bd-4871-b7c9-646371fd6075"), null, null },
                    { new Guid("8530f1c7-4873-4691-9ab3-fb22751621e8"), null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "The sand at Durankulak Beach showcases a pristine golden color, creating a beautiful contrast against the azure waters of the Black Sea.", "https://travelplanner.ro/blog/wp-content/uploads/2023/01/durankulak-beach-1-850x550.jpg.webp", null, null, "Durankulak Beach", "7699db7d-964f-4782-8209-d76562e0fece", null, new Guid("31416cb5-f3a6-4f62-8d69-8feab0abba7f"), null, null },
                    { new Guid("eb79643c-47c0-4b16-a9b7-229234880a56"), null, null, null, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "A stunning historical landmark nestled in the Rila Mountains.", "https://img.etimg.com/thumb/msid-112831459,width-640,height-480,imgsize-2180890,resizemode-4/rila-monastery-bulgaria.jpg", null, null, "Rila Monastery", "7699db7d-964f-4782-8209-d76562e0fece", null, new Guid("aaeb2ecb-afbc-43b3-a3e5-eb0439a0d995"), null, null }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7699db7d-964f-4782-8209-d76562e0fece", 0, "dda3e2e1-b68e-40fe-98eb-5cd198c3f0d5", "admin@horizons.com", true, false, null, "ADMIN@HORIZONS.COM", "ADMIN@HORIZONS.COM", "AQAAAAIAAYagAAAAEMZpM/WmTxyFMbjozvLFmvWqdZAMSzdBL6LZeOzfYMb1rfVsb8h+jBtgzCaEn2oepg==", null, false, "56f3aa16-909f-44b3-acd9-0bda929bc994", false, "admin@horizons.com" });

            migrationBuilder.InsertData(
                table: "Terrains",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3873f343-7bd1-4793-80f0-c152d91bfcb3"), new DateTime(2026, 4, 27, 9, 27, 45, 934, DateTimeKind.Utc).AddTicks(38), null, null, false, "Mountain", null },
                    { new Guid("5bd0947b-e47d-470e-96d7-eae80e99d030"), new DateTime(2026, 4, 27, 9, 27, 45, 934, DateTimeKind.Utc).AddTicks(56), null, null, false, "Plain", null },
                    { new Guid("6ecb2163-b17d-461a-92f8-d9ab07af361d"), new DateTime(2026, 4, 27, 9, 27, 45, 934, DateTimeKind.Utc).AddTicks(65), null, null, false, "Cave", null },
                    { new Guid("72620155-9f88-400a-8006-5884f1ce989f"), new DateTime(2026, 4, 27, 9, 27, 45, 934, DateTimeKind.Utc).AddTicks(59), null, null, false, "Urban", null },
                    { new Guid("9a41fd60-2b16-4230-84bd-96f335d64358"), new DateTime(2026, 4, 27, 9, 27, 45, 934, DateTimeKind.Utc).AddTicks(69), null, null, false, "Canyon", null },
                    { new Guid("a2f2d87a-c331-42b5-b327-7e608b1530e2"), new DateTime(2026, 4, 27, 9, 27, 45, 934, DateTimeKind.Utc).AddTicks(63), null, null, false, "Village", null },
                    { new Guid("d5e2ec4d-bb8a-4386-913f-3062bd639dc3"), new DateTime(2026, 4, 27, 9, 27, 45, 934, DateTimeKind.Utc).AddTicks(46), null, null, false, "Beach", null },
                    { new Guid("db29456c-4c44-4941-9a42-93a4460312ba"), new DateTime(2026, 4, 27, 9, 27, 45, 934, DateTimeKind.Utc).AddTicks(49), null, null, false, "Forest", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_AppUserId",
                table: "Destinations",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_AspNetUsers_AppUserId",
                table: "Destinations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_AspNetUsers_AppUserId",
                table: "Destinations");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_AppUserId",
                table: "Destinations");

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("0b295dfb-0e57-4155-a6c7-6d5e623d6568"));

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("14026a35-d7e6-4fa1-88bd-ba5cafcefd0e"));

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("21255192-cd9f-4d6a-9bab-701ca83d1112"));

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("8530f1c7-4873-4691-9ab3-fb22751621e8"));

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("eb79643c-47c0-4b16-a9b7-229234880a56"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("3873f343-7bd1-4793-80f0-c152d91bfcb3"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("5bd0947b-e47d-470e-96d7-eae80e99d030"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("6ecb2163-b17d-461a-92f8-d9ab07af361d"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("72620155-9f88-400a-8006-5884f1ce989f"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("9a41fd60-2b16-4230-84bd-96f335d64358"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("a2f2d87a-c331-42b5-b327-7e608b1530e2"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("d5e2ec4d-bb8a-4386-913f-3062bd639dc3"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("db29456c-4c44-4941-9a42-93a4460312ba"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Terrains");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Terrains");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Terrains");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Terrains");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Terrains");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AlternateEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Destinations",
                newName: "PublishedOn");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "UsersDestinations",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId1",
                table: "UsersDestinations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Terrains",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TerrainId",
                table: "Destinations",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Destinations",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7699db7d-964f-4782-8209-d76562e0fece", 0, "639842ca-e938-4fbc-ae53-2672aa7744d7", "admin@horizons.com", true, false, null, "ADMIN@HORIZONS.COM", "ADMIN@HORIZONS.COM", "AQAAAAIAAYagAAAAEEDclj+NdzkBSSiv1Z2sFipEBtj+W3SjIkBAQcsYEjB03xgqmXIdXSE0cyK+qpC2dQ==", null, false, "275da879-7f90-491d-b2a4-49bc0e6b7e35", false, "admin@horizons.com" });

            migrationBuilder.InsertData(
                table: "Terrains",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mountain" },
                    { 2, "Beach" },
                    { 3, "Forest" },
                    { 4, "Plain" },
                    { 5, "Urban" },
                    { 6, "Village" },
                    { 7, "Cave" },
                    { 8, "Canyon" }
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Continent", "Country", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "PublishedOn", "PublisherId", "Rating", "TerrainId", "TravelDistance" },
                values: new object[,]
                {
                    { 1, null, null, "A beautiful sunny beach with golden sands and clear waters.", "https://example.com/images/sunny_beach.jpg", null, null, "Sunny Beach", new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "7699db7d-964f-4782-8209-d76562e0fece", null, 2, null },
                    { 2, null, null, "A range of mist-covered mountains perfect for hiking and adventure.", "https://example.com/images/misty_mountains.jpg", null, null, "Misty Mountains", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7699db7d-964f-4782-8209-d76562e0fece", null, 1, null },
                    { 3, null, null, "A stunning historical landmark nestled in the Rila Mountains.", "https://img.etimg.com/thumb/msid-112831459,width-640,height-480,imgsize-2180890,resizemode-4/rila-monastery-bulgaria.jpg", null, null, "Rila Monastery", new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7699db7d-964f-4782-8209-d76562e0fece", null, 1, null },
                    { 4, null, null, "The sand at Durankulak Beach showcases a pristine golden color, creating a beautiful contrast against the azure waters of the Black Sea.", "https://travelplanner.ro/blog/wp-content/uploads/2023/01/durankulak-beach-1-850x550.jpg.webp", null, null, "Durankulak Beach", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7699db7d-964f-4782-8209-d76562e0fece", null, 2, null },
                    { 5, null, null, "A mysterious cave located in the Rhodope Mountains.", "https://detskotobnr.binar.bg/wp-content/uploads/2017/11/Diavolsko_garlo_17.jpg", null, null, "Devil's Throat Cave", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7699db7d-964f-4782-8209-d76562e0fece", null, 7, null }
                });

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
    }
}
