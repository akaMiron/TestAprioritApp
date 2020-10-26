using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TestApriorit.Data.Migrations
{
    public partial class InsertDefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Fired",
                table: "EmployeePositions",
                nullable: true);

            migrationBuilder
               .Sql("INSERT INTO Positions (Title) Values ('Junior Dev')");
            migrationBuilder
               .Sql("INSERT INTO Positions (Title) Values ('Middle Dev')");
            migrationBuilder
               .Sql("INSERT INTO Positions (Title) Values ('Senior Dev')");
            migrationBuilder
               .Sql("INSERT INTO Positions (Title) Values ('Junior QA')");
            migrationBuilder
               .Sql("INSERT INTO Positions (Title) Values ('Middle QA')");
            migrationBuilder
               .Sql("INSERT INTO Positions (Title) Values ('Senior QA')");

            migrationBuilder
               .Sql("INSERT INTO Employees (FirstName, LastName) Values ('Viktor', 'Myronenko')");
            migrationBuilder
               .Sql("INSERT INTO Employees (FirstName, LastName) Values ('Yulia', 'Myronenko')");

            migrationBuilder
               .Sql("INSERT INTO EmployeePositions (EmployeeId, PositionId, Salary, Hired) Values (" + 
               "(SELECT EmployeeId FROM Employees WHERE FirstName = 'Viktor' AND LastName = 'Myronenko')," + 
               "(SELECT PositionId FROM Positions WHERE Title = 'Junior Dev'), 700, CONVERT(date, '22-10-2020', 105))");

            migrationBuilder
               .Sql("INSERT INTO EmployeePositions (EmployeeId, PositionId, Salary, Hired) Values (" +
               "(SELECT EmployeeId FROM Employees WHERE FirstName = 'Yulia' AND LastName = 'Myronenko')," +
               "(SELECT PositionId FROM Positions WHERE Title = 'Junior QA'), 500, CONVERT(date, '25-10-2020', 105))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("if exists (select * from sysobjects where name='Employees' and xtype='U') DELETE FROM Employees");

            migrationBuilder
                .Sql("if exists (select * from sysobjects where name='Positions' and xtype='U') DELETE FROM Positions");

            migrationBuilder
                .Sql("if exists (select * from sysobjects where name='EmployeePositions' and xtype='U') DELETE FROM EmployeePositions");
        }
    }
}
