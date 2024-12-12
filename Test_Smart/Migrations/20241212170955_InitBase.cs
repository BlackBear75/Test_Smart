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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                columns: new[] { "Id", "AreaPerUnit", "CreationDate", "Deleted", "DeletionDate", "Name" },
                values: new object[,]
                {
                    { new Guid("9f9c6929-c8ed-49f3-9f2e-ff7280344589"), 70.0, new DateTime(2024, 12, 12, 17, 9, 54, 910, DateTimeKind.Utc), false, null, "Machine B" },
                    { new Guid("ec80dcee-55c6-4763-9f74-4503a105e429"), 50.0, new DateTime(2024, 12, 12, 17, 9, 54, 910, DateTimeKind.Utc), false, null, "Machine A" }
                });

            migrationBuilder.InsertData(
                table: "ProductionFacilities",
                columns: new[] { "Id", "CreationDate", "Deleted", "DeletionDate", "Name", "StandardArea" },
                values: new object[,]
                {
                    { new Guid("8e45e954-11b6-4431-9024-e79fa15a6465"), new DateTime(2024, 12, 12, 17, 9, 54, 910, DateTimeKind.Utc), false, null, "Factory B", 800.0 },
                    { new Guid("c46b2433-7736-4933-840b-82b0855779c8"), new DateTime(2024, 12, 12, 17, 9, 54, 910, DateTimeKind.Utc), false, null, "Factory A", 1000.0 }
                });

            migrationBuilder.InsertData(
                table: "PlacementContracts",
                columns: new[] { "Id", "CreationDate", "Deleted", "DeletionDate", "EquipmentTypeId", "ProductionFacilityId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("5539f319-bd82-4177-bff8-2cdd17dfa039"), new DateTime(2024, 12, 12, 17, 9, 54, 910, DateTimeKind.Utc), false, null, new Guid("ec80dcee-55c6-4763-9f74-4503a105e429"), new Guid("c46b2433-7736-4933-840b-82b0855779c8"), 10 },
                    { new Guid("d56d9d17-8884-4c18-a524-5430b62d7edf"), new DateTime(2024, 12, 12, 17, 9, 54, 910, DateTimeKind.Utc), false, null, new Guid("9f9c6929-c8ed-49f3-9f2e-ff7280344589"), new Guid("8e45e954-11b6-4431-9024-e79fa15a6465"), 5 }
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
