using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Goalsetter.DataAccess.Migrations
{
    public partial class UpdateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    VehicleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Makes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.VehicleID);
                });

            migrationBuilder.CreateTable(
                name: "VehiclePrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclePrice", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VehicleID", "CreatedDate", "IsActive", "Makes", "Model", "UpdatedDate", "Year" },
                values: new object[,]
                {
                    { new Guid("c9b113ec-d702-46cb-a4fe-175dfb446171"), new DateTime(2020, 11, 27, 0, 18, 28, 995, DateTimeKind.Utc).AddTicks(2395), true, "Chevrolet", "Cruze", new DateTime(2020, 11, 27, 0, 18, 28, 995, DateTimeKind.Utc).AddTicks(2395), 2017 },
                    { new Guid("d29bd38f-9dfb-4e99-9388-e9c097649d0b"), new DateTime(2020, 11, 27, 0, 18, 28, 996, DateTimeKind.Utc).AddTicks(5291), true, "Chevrolet", "Corsa", new DateTime(2020, 11, 27, 0, 18, 28, 996, DateTimeKind.Utc).AddTicks(5291), 2011 },
                    { new Guid("7ebed128-896e-4f05-a43d-6ee7c78d29a8"), new DateTime(2020, 11, 27, 0, 18, 28, 996, DateTimeKind.Utc).AddTicks(5310), true, "Ford", "F-100", new DateTime(2020, 11, 27, 0, 18, 28, 996, DateTimeKind.Utc).AddTicks(5310), 2000 },
                    { new Guid("e4130ceb-7482-4858-86d2-a61f517037eb"), new DateTime(2020, 11, 27, 0, 18, 28, 996, DateTimeKind.Utc).AddTicks(5314), true, "Fiat", "Palio", new DateTime(2020, 11, 27, 0, 18, 28, 996, DateTimeKind.Utc).AddTicks(5314), 2008 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "VehiclePrice");
        }
    }
}
