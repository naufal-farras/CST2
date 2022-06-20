using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CST.Migrations
{
    public partial class addNasabahIdToMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NasabahId",
                table: "M_SubSubBab",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NasabahId",
                table: "M_SubBab",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NasabahId",
                table: "M_Bab",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_M_SubSubBab_NasabahId",
                table: "M_SubSubBab",
                column: "NasabahId");

            migrationBuilder.CreateIndex(
                name: "IX_M_SubBab_NasabahId",
                table: "M_SubBab",
                column: "NasabahId");

            migrationBuilder.CreateIndex(
                name: "IX_M_Bab_NasabahId",
                table: "M_Bab",
                column: "NasabahId");

            migrationBuilder.AddForeignKey(
                name: "FK_M_Bab_M_Nasabah_NasabahId",
                table: "M_Bab",
                column: "NasabahId",
                principalTable: "M_Nasabah",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_M_SubBab_M_Nasabah_NasabahId",
                table: "M_SubBab",
                column: "NasabahId",
                principalTable: "M_Nasabah",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_M_SubSubBab_M_Nasabah_NasabahId",
                table: "M_SubSubBab",
                column: "NasabahId",
                principalTable: "M_Nasabah",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_Bab_M_Nasabah_NasabahId",
                table: "M_Bab");

            migrationBuilder.DropForeignKey(
                name: "FK_M_SubBab_M_Nasabah_NasabahId",
                table: "M_SubBab");

            migrationBuilder.DropForeignKey(
                name: "FK_M_SubSubBab_M_Nasabah_NasabahId",
                table: "M_SubSubBab");

            migrationBuilder.DropIndex(
                name: "IX_M_SubSubBab_NasabahId",
                table: "M_SubSubBab");

            migrationBuilder.DropIndex(
                name: "IX_M_SubBab_NasabahId",
                table: "M_SubBab");

            migrationBuilder.DropIndex(
                name: "IX_M_Bab_NasabahId",
                table: "M_Bab");

            migrationBuilder.DropColumn(
                name: "NasabahId",
                table: "M_SubSubBab");

            migrationBuilder.DropColumn(
                name: "NasabahId",
                table: "M_SubBab");

            migrationBuilder.DropColumn(
                name: "NasabahId",
                table: "M_Bab");
        }
    }
}
