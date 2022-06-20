using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CST.Migrations
{
    public partial class addTransDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_TransDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransaksiId = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TransDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_TransDetail_T_Transaksi_TransaksiId",
                        column: x => x.TransaksiId,
                        principalTable: "T_Transaksi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_TransDetail_TransaksiId",
                table: "T_TransDetail",
                column: "TransaksiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_TransDetail");
        }
    }
}
