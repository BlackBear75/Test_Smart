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
                    { new Guid("3acd1f1f-eff9-4d5d-9413-7a42f79b5505"), 90.0, "EQ004", new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, "Machine D" },
                    { new Guid("3c6cd489-4d2e-44bf-887e-c40595806b42"), 70.0, "EQ002", new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, "Machine B" },
                    { new Guid("426e971d-5686-4893-9ff0-57338b393744"), 30.0, "EQ003", new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, "Machine C" },
                    { new Guid("50a9bf64-de61-4953-b100-4e8942cfc0d5"), 50.0, "EQ001", new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, "Machine A" }
                });

            migrationBuilder.InsertData(
                table: "ProductionFacilities",
                columns: new[] { "Id", "Code", "CreationDate", "Deleted", "DeletionDate", "Name", "StandardArea" },
                values: new object[,]
                {
                    { new Guid("1eece660-1422-4254-8d8b-90dad255b037"), "FAC003", new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, "Factory C", 1200.0 },
                    { new Guid("d1c64f7c-379f-4340-a4aa-e48a27155849"), "FAC001", new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, "Factory A", 1000.0 },
                    { new Guid("e2b25bd1-7902-4d1d-8a80-dbb97a6f4ef2"), "FAC002", new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, "Factory B", 800.0 }
                });

            migrationBuilder.InsertData(
                table: "PlacementContracts",
                columns: new[] { "Id", "CreationDate", "Deleted", "DeletionDate", "EquipmentTypeId", "ProductionFacilityId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("45b24272-4183-46a4-9d07-655e44da32a2"), new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, new Guid("426e971d-5686-4893-9ff0-57338b393744"), new Guid("e2b25bd1-7902-4d1d-8a80-dbb97a6f4ef2"), 20 },
                    { new Guid("862afda1-b4c8-4871-8729-5db3cd231f5d"), new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, new Guid("3acd1f1f-eff9-4d5d-9413-7a42f79b5505"), new Guid("d1c64f7c-379f-4340-a4aa-e48a27155849"), 8 },
                    { new Guid("b7e98a07-1f76-4057-9974-3f4a5fa4c8ad"), new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, new Guid("426e971d-5686-4893-9ff0-57338b393744"), new Guid("1eece660-1422-4254-8d8b-90dad255b037"), 15 },
                    { new Guid("e1258336-4138-437f-9a90-c85875b6f575"), new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, new Guid("50a9bf64-de61-4953-b100-4e8942cfc0d5"), new Guid("d1c64f7c-379f-4340-a4aa-e48a27155849"), 10 },
                    { new Guid("f8ce946f-a080-413d-b3e0-409e7ccaa7b3"), new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc), false, null, new Guid("3c6cd489-4d2e-44bf-887e-c40595806b42"), new Guid("e2b25bd1-7902-4d1d-8a80-dbb97a6f4ef2"), 5 }
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
