using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspBiznes.Data.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_ClientsId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ClientsId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ClientsId",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ClientId",
                table: "CartItems",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_ClientId",
                table: "CartItems",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_ClientId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ClientId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ClientsId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ClientsId",
                table: "CartItems",
                column: "ClientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_ClientsId",
                table: "CartItems",
                column: "ClientsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
