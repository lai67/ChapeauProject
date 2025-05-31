using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace Service
{
    public class OrderService
    {

        private readonly OrderDao _dao = new();
        public Dictionary<int, (string BarStatus, string KitchenStatus)> GetTableLocationPhases()
            => _dao.GetTableLocationPhases();
      
        OrderDao orderDao;
        OrderItemDao orderItemDao;

        public OrderService()
        {
            orderDao = new OrderDao();
        }

        public int CreateOrder(Order order)
        {
            int orderId = orderDao.CreateOrder(order);
            foreach (var item in order.Items)
            {
                item.OrderId = orderId;
                orderItemDao.CreateOrderItem(item);
            }
            return orderId;
        }


    }
}
