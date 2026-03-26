using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnTapTuVung.Migrations
{
    public partial class manhdeptrai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hsk",
                columns: table => new
                {
                    IdHSK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiHSK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hsk", x => x.IdHSK);
                });

            migrationBuilder.CreateTable(
                name: "Vocabulary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHSK = table.Column<int>(type: "int", nullable: false),
                    TuVung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pinyn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TuLoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HanViet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nghia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vocabulary", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hsk");

            migrationBuilder.DropTable(
                name: "Vocabulary");
        }
    }
}
