using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Order : IEquatable<Order>
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int PreparationTime { get; set; } 
        public bool IsCreated { get; set; }
        public Employee Employee { get; set; }
  
        public Table Table { get; set; }
        public List<OrderItem> Items { get; set; }
        public Bill Bill { get; set; }

        public OrderStatus Status { get; set; }

        // for for creating a new order - This constructor is used when creating a new order in the system.
        public Order(DateTime orderTime, int preparationTime, bool isCreated, Employee employee, Table table)
        {
            OrderTime = orderTime;
            PreparationTime = preparationTime;
            IsCreated = isCreated;
            Employee = employee;
            Table = table;
            Items = new List<OrderItem>();
         
        }

        // for for loading an existing order - This constructor is used when loading an existing order from the database.
        public Order(int id, DateTime orderTime, int preparationTime, bool isCreated, Employee employee, Bill bill, Table table, List<OrderItem> items)
        {
            Id = id;
            OrderTime = orderTime;
            PreparationTime = preparationTime;
            IsCreated = isCreated;
            Employee = employee;
            Bill = bill;
            Table = table;
            Items = items;
        }

        public bool Equals(Order other)
        {
            if (other == null)
                return false;

            if (Id != other.Id ||
                OrderTime != other.OrderTime ||
                PreparationTime != other.PreparationTime ||
                Status != other.Status ||
               
                Items.Count != other.Items.Count)
            {
                return false;
            }

            return Items.SequenceEqual(other.Items);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, OrderTime, PreparationTime, Status, Bill, Items);
        }
    }


}

