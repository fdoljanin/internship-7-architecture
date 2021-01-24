using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Data.Migrations
{
    public partial class DBInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkStart = table.Column<int>(type: "int", nullable: false),
                    WorkEnd = table.Column<int>(type: "int", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleBills_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleBills_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferCategories_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceBills_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceBills_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceBills_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionBills_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionBills_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionBills_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "Cancelled", "Cost", "TransactionDate", "Type" },
                values: new object[,]
                {
                    { 1, false, 5.46m, new DateTime(2020, 12, 30, 12, 0, 1, 0, DateTimeKind.Unspecified), 0 },
                    { 2, false, 124.83m, new DateTime(2021, 1, 19, 9, 28, 17, 0, DateTimeKind.Unspecified), 0 },
                    { 3, false, 2517.87m, new DateTime(2021, 1, 19, 9, 31, 33, 0, DateTimeKind.Unspecified), 0 },
                    { 4, false, 1880.00m, new DateTime(2021, 1, 19, 10, 21, 34, 0, DateTimeKind.Unspecified), 0 },
                    { 5, false, 16.00m, new DateTime(2021, 1, 19, 14, 0, 1, 0, DateTimeKind.Unspecified), 2 },
                    { 6, false, 81.49m, new DateTime(2021, 1, 20, 10, 21, 33, 0, DateTimeKind.Unspecified), 0 },
                    { 7, false, 184.95m, new DateTime(2021, 1, 20, 10, 25, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 8, false, 210.00m, new DateTime(2021, 1, 24, 20, 39, 3, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Food" },
                    { 2, "Technology" },
                    { 3, "Literature" },
                    { 4, "Household" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FirstName", "LastName", "Pin", "isRemoved" },
                values: new object[,]
                {
                    { 3, "Megan", "Hodge", "24335495767", false },
                    { 4, "Jamie", "Witt", "00484037984", false },
                    { 1, "Owen", "Cole", "42161657377", false },
                    { 2, "Ali", "Solomon", "34148898582", false }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Pin", "WorkEnd", "WorkStart", "isRemoved" },
                values: new object[,]
                {
                    { 1, "Zara", "Fisher", "19493419882", 14, 6, false },
                    { 2, "Sean", "Hess", "24823185487", 18, 8, false },
                    { 3, "Eliza", "Martinez", "83689476125", 22, 16, false }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "IsActive", "Name", "Price", "Quantity", "Type" },
                values: new object[,]
                {
                    { 10, true, "Library access", 4.00m, 100, 2 },
                    { 9, true, "Smart Fridge", 1800.00m, 3, 0 },
                    { 8, true, "Pixel 5", 699.00m, 7, 0 },
                    { 7, true, "Haircut", 8.00m, null, 1 },
                    { 6, true, "Brown bread", 1.19m, 19, 0 },
                    { 4, true, "Magazine", 3.00m, 300, 2 },
                    { 3, true, "E-bike", 70.00m, 5, 2 },
                    { 2, true, "Nail painting", 39.99m, null, 1 },
                    { 1, true, "Chocolate bar", 1.49m, 23, 0 },
                    { 11, true, "Skin cleaning", 24.99m, null, 1 },
                    { 5, true, "White bread", 0.99m, 50, 0 },
                    { 12, true, "Introduction to Algorithms, book", 80.00m, 15, 0 }
                });

            migrationBuilder.InsertData(
                table: "ArticleBills",
                columns: new[] { "Id", "BillId", "OfferId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 3 },
                    { 3, 2, 1, 1 },
                    { 7, 3, 1, 12 },
                    { 12, 6, 1, 2 },
                    { 9, 4, 12, 1 },
                    { 10, 4, 9, 1 },
                    { 8, 3, 9, 1 },
                    { 2, 1, 5, 1 },
                    { 5, 2, 5, 1 },
                    { 4, 2, 6, 2 },
                    { 11, 6, 12, 1 },
                    { 6, 3, 8, 1 }
                });

            migrationBuilder.InsertData(
                table: "OfferCategories",
                columns: new[] { "Id", "CategoryId", "OfferId" },
                values: new object[,]
                {
                    { 7, 3, 10 },
                    { 10, 4, 9 },
                    { 6, 2, 9 },
                    { 5, 2, 8 },
                    { 3, 1, 6 },
                    { 2, 1, 5 },
                    { 4, 2, 3 },
                    { 1, 1, 1 },
                    { 8, 3, 12 }
                });

            migrationBuilder.InsertData(
                table: "ServiceBills",
                columns: new[] { "Id", "BillId", "Duration", "EmployeeId", "OfferId", "StartTime" },
                values: new object[,]
                {
                    { 3, 7, 4, 2, 2, new DateTime(2021, 1, 20, 8, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, 3, 1, 2, new DateTime(2021, 1, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 7, 1, 3, 11, new DateTime(2021, 1, 28, 17, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 5, 2, 1, 7, new DateTime(2021, 1, 23, 9, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionBills",
                columns: new[] { "Id", "BillId", "CustomerId", "OfferId", "StartTime" },
                values: new object[,]
                {
                    { 1, null, 1, 4, new DateTime(2020, 7, 2, 11, 11, 12, 0, DateTimeKind.Unspecified) },
                    { 4, 8, 3, 3, new DateTime(2020, 11, 1, 18, 12, 22, 0, DateTimeKind.Unspecified) },
                    { 2, null, 1, 10, new DateTime(2020, 7, 6, 13, 14, 15, 0, DateTimeKind.Unspecified) },
                    { 3, null, 2, 10, new DateTime(2020, 9, 1, 13, 0, 7, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleBills_BillId",
                table: "ArticleBills",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleBills_OfferId",
                table: "ArticleBills",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferCategories_CategoryId",
                table: "OfferCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferCategories_OfferId",
                table: "OfferCategories",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBills_BillId",
                table: "ServiceBills",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBills_EmployeeId",
                table: "ServiceBills",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBills_OfferId",
                table: "ServiceBills",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionBills_BillId",
                table: "SubscriptionBills",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionBills_CustomerId",
                table: "SubscriptionBills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionBills_OfferId",
                table: "SubscriptionBills",
                column: "OfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleBills");

            migrationBuilder.DropTable(
                name: "OfferCategories");

            migrationBuilder.DropTable(
                name: "ServiceBills");

            migrationBuilder.DropTable(
                name: "SubscriptionBills");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Offers");
        }
    }
}
