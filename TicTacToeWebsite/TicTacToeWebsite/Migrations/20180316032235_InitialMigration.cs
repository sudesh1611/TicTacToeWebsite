using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TicTacToeWebsite.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfirmPassword = table.Column<string>(nullable: false),
                    DoubleLoses = table.Column<long>(nullable: false),
                    DoubleTies = table.Column<long>(nullable: false),
                    DoubleWins = table.Column<long>(nullable: false),
                    EmailID = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    SingleEasyLoses = table.Column<long>(nullable: false),
                    SingleEasyTies = table.Column<long>(nullable: false),
                    SingleEasyWins = table.Column<long>(nullable: false),
                    SingleHardLoses = table.Column<long>(nullable: false),
                    SingleHardTies = table.Column<long>(nullable: false),
                    SingleHardWins = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccount");
        }
    }
}
