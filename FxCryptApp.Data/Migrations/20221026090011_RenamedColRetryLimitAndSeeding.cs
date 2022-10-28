using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FxCryptApp.Data.Migrations
{
    public partial class RenamedColRetryLimitAndSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LimitPerMinute",
                table: "TickerSource",
                newName: "RetryLimit");

            migrationBuilder.InsertData(
                table: "TickerSource",
                columns: new[] { "Id", "EndpointUrl", "Name", "RetryLimit" },
                values: new object[,]
                {
                    { 1L, "https://www.bitstamp.net/api/v2/ticker/btcusd/", "Bitstamp API", 3L },
                    { 2L, "https://api.bitfinex.com/v1/pubticker/btcusd/", "Bitfinex API", 3L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TickerSource",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "TickerSource",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.RenameColumn(
                name: "RetryLimit",
                table: "TickerSource",
                newName: "LimitPerMinute");
        }
    }
}
