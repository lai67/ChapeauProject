using System;

namespace Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int PreparationTime { get; set; } 
        public bool IsCreated { get; set; }
        public Employee Employee { get; set; }
  
        public Table Table { get; set; }
        public List<OrderItem> Items { get; set; }
        public Bill Bill { get; set; }

        // ctor for creating a new order - This constructor is used when creating a new order in the system.
        public Order(DateTime orderTime, int preparationTime, bool isCreated, Employee employee, Table table)
        {
            OrderTime = orderTime;
            PreparationTime = preparationTime;
            IsCreated = isCreated;
            Employee = employee;
            Table = table;
            Items = new List<OrderItem>();
        }

        // ctor for loading an existing order - This constructor is used when loading an existing order from the database.
        public Order(int id, DateTime orderTime, int preparationTime, bool isCreated, Employee employee, Bill bill, Table table)
        {
            Id = id;
            OrderTime = orderTime;
            PreparationTime = preparationTime;
            IsCreated = isCreated;
            Employee = employee;
            Bill = bill;
            Table = table;
            Items = new List<OrderItem>();
        }

  
    }


}

