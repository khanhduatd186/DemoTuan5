using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoTuan5.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoTuan5Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoTuan5Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DemoTuan5Warehouses",
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
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoTuan5Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DemoTuan5WarehouseLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoTuan5WarehouseLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemoTuan5WarehouseLocations_DemoTuan5Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "DemoTuan5Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DemoTuan5WarehouseLocations_DemoTuan5Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "DemoTuan5Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemoTuan5Countries_Code",
                table: "DemoTuan5Countries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DemoTuan5WarehouseLocations_CountryId",
                table: "DemoTuan5WarehouseLocations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DemoTuan5WarehouseLocations_WarehouseId",
                table: "DemoTuan5WarehouseLocations",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_DemoTuan5Warehouses_Code",
                table: "DemoTuan5Warehouses",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoTuan5WarehouseLocations");

            migrationBuilder.DropTable(
                name: "DemoTuan5Countries");

            migrationBuilder.DropTable(
                name: "DemoTuan5Warehouses");
        }
    }
}
