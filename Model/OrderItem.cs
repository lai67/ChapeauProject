

using static Model.Order;


namespace Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public MenuItemModel MenuItem { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public string Comment { get; set; } // Comment for the order item
        public OrderStatus orderStatus { get; set; } // Status of the order item

        // Constructor - This constructor is used when creating a new order item in the system.
        public OrderItem(MenuItemModel menuItem, string comment, OrderStatus orderStatus, int count, int orderId)
        {
            MenuItem = menuItem;
            Comment = comment;
            this.orderStatus = orderStatus;
            Count = count;
            OrderId = orderId;
        }
        // Constructor for loading an existing order item - This constructor is used when loading an existing order item from the database.
        public OrderItem(int id, MenuItemModel menuItem, string comment, OrderStatus orderStatus, int count,int orderId)
        {
            Id = id;
            MenuItem = menuItem;
            Comment = comment;
            this.orderStatus = orderStatus;
            Count = count;
            OrderId = orderId;
        }

        public enum OrderStatus
        {
            Placed, Preparing, Ready, Served
        }

    }
}