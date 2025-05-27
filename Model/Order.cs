using System;

namespace Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int PreparationTime { get; set; } // preparation changed to int instead of DateTime
        public string Status { get; set; } // Should match the enum in OrderDao
        public Employee Employee { get; set; }
        public string PreparationLocation { get; set; }
        public Table Table { get; set; }
        public List<OrderItem> Items { get; set; }

        public Bill Bill { get; set; }

        // ctor for creating a new order - This constructor is used when creating a new order in the system.
        public Order(DateTime orderTime, int preparationTime, string status, Employee employee, Bill bill, string preparationLocation, Table table)
        {
            OrderTime = orderTime;
            PreparationTime = preparationTime;
            Status = status;
            Employee = employee;
            Bill = bill;
            PreparationLocation = preparationLocation;
            Table = table;
            Items = new List<OrderItem>();
        }

        // ctor for loading an existing order - This constructor is used when loading an existing order from the database.
        public Order(int id, DateTime orderTime, int preparationTime, string status, Employee employee, Bill bill, string preparationLocation, Table table)
        {
            Id = id;
            OrderTime = orderTime;
            PreparationTime = preparationTime;
            Status = status;
            Employee = employee;
            Bill = bill;
            PreparationLocation = preparationLocation;
            Table = table;
            Items = new List<OrderItem>();
        }

        // Enum for order status
        public enum OrderStatus
        {
            Placed, Preparing, Ready, Served
        }


    }


}