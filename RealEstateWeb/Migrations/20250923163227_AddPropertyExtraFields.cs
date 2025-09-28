using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyExtraFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Properties",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "Bath",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Bed",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Properties",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Properties",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Bath", "Bed", "Description", "ImageUrl", "Price", "Size", "Status", "Title", "Type" },
                values: new object[] { "123 Street, New York, USA", 2, 3, "Nhà phố đẹp, trung tâm thành phố, nội thất cao cấp.", "/img/property-1.jpg", 12345000m, 1000, "For Sell", "Golden Urban House For Sell", "Apartment" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Bath", "Bed", "Description", "ImageUrl", "Price", "Size", "Status", "Title", "Type" },
                values: new object[] { "456 Avenue, Los Angeles, USA", 4, 5, "Biệt thự sang trọng, view sông, đầy đủ tiện nghi.", "/img/property-2.jpg", 8500000m, 2500, "For Rent", "Luxury Villa For Rent", "Villa" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "Bath", "Bed", "Description", "ImageUrl", "Price", "Size", "Status", "Title", "Type" },
                values: new object[] { 3, "789 Business Rd, Chicago, USA", 2, 0, "Văn phòng hiện đại nằm tại khu business district.", "/img/property-3.jpg", 4500000m, 1500, "For Sell", "Modern Office Space", "Office" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Bath",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Bed",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Properties",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { "123 Đường Lê Lợi, Quận 1, TP.HCM", "Nhà đẹp trung tâm Quận 1", "/img/nhaphoquan1.jpg", 5000000000m, "Nhà phố Quận 1" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { "456 Đường Nguyễn Văn Linh, Quận 7, TP.HCM", "Căn hộ view sông Quận 7", "/img/canhoquan7.jpg", 2000000000m, "Căn hộ Quận 7" });
        }
    }
}
