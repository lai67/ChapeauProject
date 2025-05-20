using System;

namespace Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int PreparationTime { get; set; } // preparation changed to int instead of DateTime
        public string Status { get; set; } // Should match the enum in OrderDao
        public int Employee { get; set; }
        public int Bill { get; set; }
        public string PreparationLocation { get; set; }
        public int Table { get; set; }
    }
}