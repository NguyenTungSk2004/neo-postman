using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class Update_Ref_UserVerificationToken_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserVerificationTokens_UserId",
                table: "UserVerificationTokens");

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationTokens_UserId",
                table: "UserVerificationTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserVerificationTokens_UserId",
                table: "UserVerificationTokens");

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationTokens_UserId",
                table: "UserVerificationTokens",
                column: "UserId",
                unique: true);
        }
    }
}
