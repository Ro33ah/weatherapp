using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weatherapp.Migrations
{
    public partial class weatherappModelsApplicationContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    cityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.cityId);
                });

            migrationBuilder.CreateTable(
                name: "Mains",
                columns: table => new
                {
                    mainId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    temp = table.Column<float>(nullable: false),
                    humidity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mains", x => x.mainId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherModels",
                columns: table => new
                {
                    weatherModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherModels", x => x.weatherModelId);
                    table.ForeignKey(
                        name: "FK_WeatherModels_Cities_cityId",
                        column: x => x.cityId,
                        principalTable: "Cities",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    readingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    mainId = table.Column<int>(nullable: true),
                    dt = table.Column<long>(nullable: false),
                    weatherModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.readingId);
                    table.ForeignKey(
                        name: "FK_Readings_Mains_mainId",
                        column: x => x.mainId,
                        principalTable: "Mains",
                        principalColumn: "mainId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Readings_WeatherModels_weatherModelId",
                        column: x => x.weatherModelId,
                        principalTable: "WeatherModels",
                        principalColumn: "weatherModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Readings_mainId",
                table: "Readings",
                column: "mainId");

            migrationBuilder.CreateIndex(
                name: "IX_Readings_weatherModelId",
                table: "Readings",
                column: "weatherModelId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherModels_cityId",
                table: "WeatherModels",
                column: "cityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Readings");

            migrationBuilder.DropTable(
                name: "Mains");

            migrationBuilder.DropTable(
                name: "WeatherModels");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
