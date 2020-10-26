using System;
using System.Diagnostics.CodeAnalysis;

namespace TestApriorit.Core.Models
{
    public class EmployeePosition
    {
        public int EmployeePositionId { get; set; }
        public int Salary { get; set; }
        public DateTime Hired { get; set; }
        [AllowNull]
        public DateTime? Fired { get; set; }
        // Relations
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
