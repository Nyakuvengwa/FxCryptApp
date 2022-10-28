using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FxCryptApp.Data.Migrations
{
    public partial class AddedBtcUsdTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BtcUsdPrice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    SeededDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TickerSourceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BtcUsdPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BtcUsdPrice_TickerSource_TickerSourceId",
                        column: x => x.TickerSourceId,
                        principalTable: "TickerSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BtcUsdPrice_TickerSourceId",
                table: "BtcUsdPrice",
                column: "TickerSourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BtcUsdPrice");
        }
    }
}
