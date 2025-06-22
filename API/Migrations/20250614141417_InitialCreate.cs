using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_PAM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USUARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Foto = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIOS", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TB_USUARIOS",
                columns: new[] { "Id", "Email", "Foto", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, "seuEmail@example.com", "https://thvnext.bing.com/th/id/OIP.fU7XmhYQvxJs89FnvKwgigHaEk?cb=thvnext&rs=1&pid=ImgDetMain", new byte[] { 110, 131, 36, 76, 52, 64, 76, 173, 59, 89, 99, 181, 43, 16, 223, 224, 52, 104, 0, 23, 195, 197, 7, 70, 78, 54, 235, 146, 154, 72, 121, 42, 179, 202, 166, 19, 252, 66, 181, 249, 220, 139, 237, 8, 190, 132, 250, 15, 245, 130, 203, 211, 192, 124, 12, 25, 138, 196, 59, 211, 76, 167, 105, 75 }, new byte[] { 180, 125, 173, 82, 194, 1, 181, 46, 201, 21, 70, 10, 217, 90, 135, 73, 185, 242, 71, 74, 149, 36, 97, 86, 96, 222, 105, 82, 235, 231, 65, 123, 124, 223, 103, 245, 77, 74, 163, 20, 255, 65, 147, 30, 135, 6, 53, 250, 109, 82, 131, 254, 124, 200, 141, 242, 114, 231, 210, 38, 247, 252, 219, 126, 52, 188, 53, 26, 234, 18, 107, 233, 248, 117, 108, 153, 165, 25, 34, 38, 81, 136, 225, 70, 91, 28, 11, 116, 85, 212, 23, 34, 115, 86, 248, 138, 96, 36, 1, 63, 38, 77, 61, 212, 169, 227, 143, 116, 89, 154, 32, 80, 105, 33, 66, 124, 1, 42, 120, 83, 134, 227, 19, 203, 229, 149, 131, 158 }, "UsuarioAdmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USUARIOS");
        }
    }
}
