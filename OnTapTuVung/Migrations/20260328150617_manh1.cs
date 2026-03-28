using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OnTapTuVung.Migrations
{
    public partial class manh1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HSKs",
                columns: table => new
                {
                    IdHSK = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoaiHSK = table.Column<int>(type: "integer", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HSKs", x => x.IdHSK);
                });

            migrationBuilder.CreateTable(
                name: "Vocabularies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdHSK = table.Column<int>(type: "integer", nullable: false),
                    TuVung = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Pinyn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TuLoai = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HanViet = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Nghia = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vocabularies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vocabularies_HSKs_IdHSK",
                        column: x => x.IdHSK,
                        principalTable: "HSKs",
                        principalColumn: "IdHSK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HSKs",
                columns: new[] { "IdHSK", "LoaiHSK" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_IdHSK",
                table: "Vocabularies",
                column: "IdHSK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vocabularies");

            migrationBuilder.DropTable(
                name: "HSKs");
        }
    }
}
