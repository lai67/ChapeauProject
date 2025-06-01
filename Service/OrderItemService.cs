using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace Service
{

    public class OrderItemService
    {
        OrderItemDao OrderItemDao;
        MenuItemDao MenuItemDao;
        public OrderItemService() 
        {
            OrderItemDao = new OrderItemDao();
            MenuItemDao = new MenuItemDao();
        }

        // Create Order Item
        public bool CreateOrderItem(OrderItem orderItem)
        {
            int stock = MenuItemDao.CheckMenuItemStock(orderItem.MenuItem.Id);
            if (orderItem.Count > stock)
            {
                return false;
            }
            OrderItemDao.CreateOrderItem(orderItem);
            return true;

        }
        // Get Order Items by Order Id
        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return OrderItemDao.GetOrderItemsByOrderId(orderId);
        }

        // Update Order Item Count
        public bool UpdateOrderItemCount(Model.OrderItem orderItem)
        {
            int stock = MenuItemDao.CheckMenuItemStock(orderItem.MenuItem.Id);
            if(orderItem.Count > stock)
            {
                return false;
            }
            OrderItemDao.UpdateOrderItemCount(orderItem);
            return true;
        }
    }
}
