using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class modifyonRelationshipBetweenAddressAndAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Addresses_AddressId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Addresses_AddressId",
                table: "Accounts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Addresses_AddressId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Addresses_AddressId",
                table: "Accounts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
