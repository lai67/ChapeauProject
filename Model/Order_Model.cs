using System;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime PreparationTime { get; set; }
        public string Status { get; set; } // Should match the enum in OrderDao
        public int EmployeeId { get; set; }
        public int BillId { get; set; }
        public string PreparationLocation { get; set; }
        public int TableId { get; set; }
    }
}