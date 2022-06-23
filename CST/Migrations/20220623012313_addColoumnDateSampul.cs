using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CST.Migrations
{
    public partial class addColoumnDateSampul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TanggalSampul",
                table: "T_Transaksi",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TanggalSampul",
                table: "T_Transaksi");
        }
    }
}
