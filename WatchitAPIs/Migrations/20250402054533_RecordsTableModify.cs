using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchitAPIs.Migrations
{
    /// <inheritdoc />
    public partial class RecordsTableModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Records");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Records",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                columns: new[] { "UserId", "MovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_Records_MovieId",
                table: "Records",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Movies_MovieId",
                table: "Records",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Movies_MovieId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_MovieId",
                table: "Records");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "Id");
        }
    }
}
