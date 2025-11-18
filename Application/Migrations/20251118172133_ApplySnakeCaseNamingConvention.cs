using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class ApplySnakeCaseNamingConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemTypes",
                table: "ItemTypes");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "items");

            migrationBuilder.RenameTable(
                name: "ItemTypes",
                newName: "item_types");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "items",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "items",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "items",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "items",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "SerialNumber",
                table: "items",
                newName: "serial_number");

            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "items",
                newName: "purchase_date");

            migrationBuilder.RenameColumn(
                name: "ItemTypeId",
                table: "items",
                newName: "item_type_id");

            migrationBuilder.RenameColumn(
                name: "HolderEmployeeId",
                table: "items",
                newName: "holder_employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_Items_Id",
                table: "items",
                newName: "ix_items_id");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemTypeId",
                table: "items",
                newName: "ix_items_item_type_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "item_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "item_types",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "item_types",
                newName: "tenant_id");

            migrationBuilder.RenameIndex(
                name: "IX_ItemTypes_Name",
                table: "item_types",
                newName: "ix_item_types_name");

            migrationBuilder.AddPrimaryKey(
                name: "pk_items",
                table: "items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_item_types",
                table: "item_types",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_items_item_types_item_type_id",
                table: "items",
                column: "item_type_id",
                principalTable: "item_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_items_item_types_item_type_id",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_items",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_item_types",
                table: "item_types");

            migrationBuilder.RenameTable(
                name: "items",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "item_types",
                newName: "ItemTypes");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Items",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Items",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Items",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Items",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "Items",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "serial_number",
                table: "Items",
                newName: "SerialNumber");

            migrationBuilder.RenameColumn(
                name: "purchase_date",
                table: "Items",
                newName: "PurchaseDate");

            migrationBuilder.RenameColumn(
                name: "item_type_id",
                table: "Items",
                newName: "ItemTypeId");

            migrationBuilder.RenameColumn(
                name: "holder_employee_id",
                table: "Items",
                newName: "HolderEmployeeId");

            migrationBuilder.RenameIndex(
                name: "ix_items_id",
                table: "Items",
                newName: "IX_Items_Id");

            migrationBuilder.RenameIndex(
                name: "ix_items_item_type_id",
                table: "Items",
                newName: "IX_Items_ItemTypeId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ItemTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ItemTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "ItemTypes",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "ix_item_types_name",
                table: "ItemTypes",
                newName: "IX_ItemTypes_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemTypes",
                table: "ItemTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeId",
                table: "Items",
                column: "ItemTypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
