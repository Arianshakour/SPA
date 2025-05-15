using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneBooks",
                columns: table => new
                {
                    PhonebookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phonebook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneBooks", x => x.PhonebookId);
                    table.ForeignKey(
                        name: "FK_PhoneBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneBooks_UserId",
                table: "PhoneBooks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneBooks");
        }
    }
}
