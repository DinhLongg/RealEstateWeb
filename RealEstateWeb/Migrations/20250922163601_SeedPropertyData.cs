using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstateWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedPropertyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "123 Đường Lê Lợi, Quận 1, TP.HCM", "Nhà đẹp trung tâm Quận 1", "/img/nhaphoquan1.jpg", 5000000000m, "Nhà phố Quận 1" },
                    { 2, "456 Đường Nguyễn Văn Linh, Quận 7, TP.HCM", "Căn hộ view sông Quận 7", "/img/canhoquan7.jpg", 2000000000m, "Căn hộ Quận 7" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
