using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test_Smart.Migrations
{
    /// <inheritdoc />
    public partial class InitBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AreaPerUnit = table.Column<double>(type: "float", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionFacilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StandardArea = table.Column<double>(type: "float", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlacementContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacementContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacementContracts_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacementContracts_ProductionFacilities_ProductionFacilityId",
                        column: x => x.ProductionFacilityId,
                        principalTable: "ProductionFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EquipmentTypes",
                columns: new[] { "Id", "AreaPerUnit", "Code", "CreationDate", "Deleted", "DeletionDate", "Name" },
                values: new object[,]
                {
                    { new Guid("78e6bacc-22fe-4cc9-9e2b-b2fef1dc1b62"), 70.0, "EQ002", new DateTime(2024, 12, 12, 17, 47, 46, 300, DateTimeKind.Utc), false, null, "Machine B" },
                    { new Guid("fd5fbc9e-f75c-4de7-a45c-de3fadbb14b4"), 50.0, "EQ001", new DateTime(2024, 12, 12, 17, 47, 46, 300, DateTimeKind.Utc), false, null, "Machine A" }
                });

            migrationBuilder.InsertData(
                table: "ProductionFacilities",
                columns: new[] { "Id", "Code", "CreationDate", "Deleted", "DeletionDate", "Name", "StandardArea" },
                values: new object[,]
                {
                    { new Guid("40d0a731-9c98-4126-9464-7a35100c59b8"), "FAC001", new DateTime(2024, 12, 12, 17, 47, 46, 300, DateTimeKind.Utc), false, null, "Factory A", 1000.0 },
                    { new Guid("5f29a2ec-2936-4720-938e-54022329593c"), "FAC002", new DateTime(2024, 12, 12, 17, 47, 46, 300, DateTimeKind.Utc), false, null, "Factory B", 800.0 }
                });

            migrationBuilder.InsertData(
                table: "PlacementContracts",
                columns: new[] { "Id", "CreationDate", "Deleted", "DeletionDate", "EquipmentTypeId", "ProductionFacilityId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("337cb304-09ae-4f46-973d-0718ef2a6e03"), new DateTime(2024, 12, 12, 17, 47, 46, 300, DateTimeKind.Utc), false, null, new Guid("fd5fbc9e-f75c-4de7-a45c-de3fadbb14b4"), new Guid("40d0a731-9c98-4126-9464-7a35100c59b8"), 10 },
                    { new Guid("acaf647a-920b-4b88-95e5-4c76a990198c"), new DateTime(2024, 12, 12, 17, 47, 46, 300, DateTimeKind.Utc), false, null, new Guid("78e6bacc-22fe-4cc9-9e2b-b2fef1dc1b62"), new Guid("5f29a2ec-2936-4720-938e-54022329593c"), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_EquipmentTypeId",
                table: "PlacementContracts",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_ProductionFacilityId",
                table: "PlacementContracts",
                column: "ProductionFacilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlacementContracts");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "ProductionFacilities");
        }
    }
}
