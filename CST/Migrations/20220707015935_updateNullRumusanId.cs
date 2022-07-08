using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CST.Migrations
{
    public partial class updateNullRumusanId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Transaksi_T_RumusanNasabah_RumusanNasabahId",
                table: "T_Transaksi");

            migrationBuilder.AlterColumn<int>(
                name: "RumusanNasabahId",
                table: "T_Transaksi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Transaksi_T_RumusanNasabah_RumusanNasabahId",
                table: "T_Transaksi",
                column: "RumusanNasabahId",
                principalTable: "T_RumusanNasabah",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Transaksi_T_RumusanNasabah_RumusanNasabahId",
                table: "T_Transaksi");

            migrationBuilder.AlterColumn<int>(
                name: "RumusanNasabahId",
                table: "T_Transaksi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_T_Transaksi_T_RumusanNasabah_RumusanNasabahId",
                table: "T_Transaksi",
                column: "RumusanNasabahId",
                principalTable: "T_RumusanNasabah",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
