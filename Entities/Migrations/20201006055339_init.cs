using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contributor",
                columns: table => new
                {
                    ContributorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    NationalCode = table.Column<string>(maxLength: 15, nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributor", x => x.ContributorId);
                });

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    MeetingId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingTitle = table.Column<string>(maxLength: 512, nullable: true),
                    MeetingDate = table.Column<long>(nullable: true),
                    MeetingPlace = table.Column<string>(maxLength: 512, nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CreateDate = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.MeetingId);
                });

            migrationBuilder.CreateTable(
                name: "Speaker",
                columns: table => new
                {
                    SpeakerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaker", x => x.SpeakerId);
                });

            migrationBuilder.CreateTable(
                name: "MeetingTicket",
                columns: table => new
                {
                    MeetingTicketId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingId = table.Column<long>(nullable: true),
                    Count = table.Column<int>(nullable: true),
                    Price = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingTicket", x => x.MeetingTicketId);
                    table.ForeignKey(
                        name: "FK_MeetingTicket_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meeting",
                        principalColumn: "MeetingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingSpeaker",
                columns: table => new
                {
                    MeetingSpeakerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingId = table.Column<long>(nullable: true),
                    SpeakerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingSpeaker", x => x.MeetingSpeakerId);
                    table.ForeignKey(
                        name: "FK_MeetingSpeaker_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meeting",
                        principalColumn: "MeetingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingSpeaker_Speaker",
                        column: x => x.SpeakerId,
                        principalTable: "Speaker",
                        principalColumn: "SpeakerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContributorTicket",
                columns: table => new
                {
                    ContributorTicketId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContributorId = table.Column<long>(nullable: true),
                    MeetingTicketId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorTicket", x => x.ContributorTicketId);
                    table.ForeignKey(
                        name: "FK_ContributorTicket_Contributor",
                        column: x => x.ContributorId,
                        principalTable: "Contributor",
                        principalColumn: "ContributorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContributorTicket_MeetingTicket",
                        column: x => x.MeetingTicketId,
                        principalTable: "MeetingTicket",
                        principalColumn: "MeetingTicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContributorPayment",
                columns: table => new
                {
                    ContributorPaymentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContributorTicketId = table.Column<long>(nullable: true),
                    transactionCode = table.Column<string>(maxLength: 100, nullable: true),
                    TransactionDate = table.Column<long>(nullable: true),
                    Amount = table.Column<long>(nullable: true),
                    RefId = table.Column<int>(nullable: true),
                    FinalStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorPayment", x => x.ContributorPaymentId);
                    table.ForeignKey(
                        name: "FK_ContributorPayment_ContributorTicket",
                        column: x => x.ContributorTicketId,
                        principalTable: "ContributorTicket",
                        principalColumn: "ContributorTicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContributorPayment_ContributorTicketId",
                table: "ContributorPayment",
                column: "ContributorTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorTicket_ContributorId",
                table: "ContributorTicket",
                column: "ContributorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorTicket_MeetingTicketId",
                table: "ContributorTicket",
                column: "MeetingTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSpeaker_MeetingId",
                table: "MeetingSpeaker",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSpeaker_SpeakerId",
                table: "MeetingSpeaker",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTicket_MeetingId",
                table: "MeetingTicket",
                column: "MeetingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContributorPayment");

            migrationBuilder.DropTable(
                name: "MeetingSpeaker");

            migrationBuilder.DropTable(
                name: "ContributorTicket");

            migrationBuilder.DropTable(
                name: "Speaker");

            migrationBuilder.DropTable(
                name: "Contributor");

            migrationBuilder.DropTable(
                name: "MeetingTicket");

            migrationBuilder.DropTable(
                name: "Meeting");
        }
    }
}
