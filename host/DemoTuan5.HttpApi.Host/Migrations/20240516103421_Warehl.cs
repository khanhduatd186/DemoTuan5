using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoTuan5.Migrations
{
    /// <inheritdoc />
    public partial class Warehl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoTuan5WarehouseLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Idx = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoTuan5WarehouseLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemoTuan5WarehouseLocations_DemoTuan5Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "DemoTuan5Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemoTuan5WarehouseLocations_DemoTuan5Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "DemoTuan5Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemoTuan5WarehouseLocations_CountryId",
                table: "DemoTuan5WarehouseLocations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DemoTuan5WarehouseLocations_WarehouseId",
                table: "DemoTuan5WarehouseLocations",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoTuan5WarehouseLocations");
        }
    }
}
