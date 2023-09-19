using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP.Service.DataAccessLayer.Migrations
{
    public partial class RenameProviderCustomerUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Services",
                newName: "ProviderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ProviderId",
                table: "Services",
                newName: "IX_Services_ProviderUserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Events",
                newName: "CustomerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderUserId",
                table: "Services",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ProviderUserId",
                table: "Services",
                newName: "IX_Services_ProviderId");

            migrationBuilder.RenameColumn(
                name: "CustomerUserId",
                table: "Events",
                newName: "CustomerId");
        }
    }
}
