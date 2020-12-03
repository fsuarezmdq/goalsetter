using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Goalsetter.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Makes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehiclePrice",
                columns: table => new
                {
                    VehiclePriceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclePrice", x => x.VehiclePriceID);
                    table.ForeignKey(
                        name: "FK_VehiclePrice_Vehicles_VehiclePriceID",
                        column: x => x.VehiclePriceID,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientID", "ClientName", "CreatedDate", "Email", "IsActive", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("6238554c-2d3c-4e25-a2dc-9ed13dfb570e"), "José de San Martin", new DateTime(2020, 12, 3, 0, 20, 12, 711, DateTimeKind.Utc).AddTicks(5489), "jose@sanmartin.com", true, new DateTime(2020, 12, 3, 0, 20, 12, 711, DateTimeKind.Utc).AddTicks(5498) },
                    { new Guid("88393b8d-aa92-4914-95d4-6523f9006252"), "Mariano Moreno", new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3979), "marian@moreno.com", true, new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3980) },
                    { new Guid("060647f4-0f55-43e3-8bcd-b4311bbcc962"), "Juan José Castelli", new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(4007), "jose@castelli.com", true, new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(4008) },
                    { new Guid("5e96745c-2a42-4e5b-ae74-ea834d673f48"), "Domingo Faustino Sarmiento", new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(4025), "domi@sarmiento.com", true, new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(4026) }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Makes", "Model", "UpdatedDate", "Year" },
                values: new object[,]
                {
                    { new Guid("bcddd166-c7df-49b0-986c-b63880a234d8"), new DateTime(2020, 12, 3, 0, 20, 12, 701, DateTimeKind.Utc).AddTicks(3991), true, "Chevrolet", "Cruze", new DateTime(2020, 12, 3, 0, 20, 12, 701, DateTimeKind.Utc).AddTicks(3995), 2017 },
                    { new Guid("37b3fe05-d5a1-4c57-9946-589ff756e0d3"), new DateTime(2020, 12, 3, 0, 20, 12, 712, DateTimeKind.Utc).AddTicks(2809), true, "Chevrolet", "Corsa", new DateTime(2020, 12, 3, 0, 20, 12, 712, DateTimeKind.Utc).AddTicks(2831), 2011 },
                    { new Guid("0934e16d-6c45-4286-8568-472a017bd7f9"), new DateTime(2020, 12, 3, 0, 20, 12, 712, DateTimeKind.Utc).AddTicks(2834), true, "Ford", "F-100", new DateTime(2020, 12, 3, 0, 20, 12, 712, DateTimeKind.Utc).AddTicks(2837), 2000 },
                    { new Guid("23345e2f-0f9a-4f10-bd57-c769d53b184f"), new DateTime(2020, 12, 3, 0, 20, 12, 712, DateTimeKind.Utc).AddTicks(2839), true, "Fiat", "Palio", new DateTime(2020, 12, 3, 0, 20, 12, 712, DateTimeKind.Utc).AddTicks(2843), 2008 }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "ClientId", "CreatedDate", "IsActive", "TotalPrice", "VehicleId", "StartDate", "EndDate" },
                values: new object[] { new Guid("698258e4-8246-4a3d-8e0c-a974e532ccf5"), new Guid("6238554c-2d3c-4e25-a2dc-9ed13dfb570e"), new DateTime(2020, 12, 3, 0, 20, 12, 715, DateTimeKind.Utc).AddTicks(2990), true, 2100m, new Guid("bcddd166-c7df-49b0-986c-b63880a234d8"), new DateTime(2022,1,1), new DateTime(2022,1,15) });

            migrationBuilder.InsertData(
                table: "VehiclePrice",
                columns: new[] { "VehiclePriceID", "CreatedDate", "IsActive", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("bcddd166-c7df-49b0-986c-b63880a234d8"), new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3131), true, 150m, new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3134) },
                    { new Guid("37b3fe05-d5a1-4c57-9946-589ff756e0d3"), new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3141), true, 100m, new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3142) },
                    { new Guid("0934e16d-6c45-4286-8568-472a017bd7f9"), new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3146), true, 120m, new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3146) },
                    { new Guid("23345e2f-0f9a-4f10-bd57-c769d53b184f"), new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3150), true, 80m, new DateTime(2020, 12, 3, 0, 20, 12, 713, DateTimeKind.Utc).AddTicks(3150) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ClientId",
                table: "Rentals",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_VehicleId",
                table: "Rentals",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "VehiclePrice");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
