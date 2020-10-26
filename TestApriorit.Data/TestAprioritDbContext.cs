using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestApriorit.Core.Models;
using TestApriorit.Data.Configurations;

namespace TestApriorit.Data
{
    public class TestAprioritDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }

        public TestAprioritDbContext(DbContextOptions<TestAprioritDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new EmployeeConfiguration());

            builder
                .ApplyConfiguration(new PositionConfiguration());

            builder
                .ApplyConfiguration(new EmployeePositionConfiguration());
        }
    }
}
