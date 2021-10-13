using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanhiaAguasWebMVC.Migrations
{
    public partial class UpdateConsumption3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumptions_Clients_ClientId",
                table: "Consumptions");

            migrationBuilder.DropIndex(
                name: "IX_Consumptions_ClientId",
                table: "Consumptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_ClientId",
                table: "Consumptions",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumptions_Clients_ClientId",
                table: "Consumptions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
