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
        private readonly OrderDao _orderDao;
        private readonly OrderItemDao _orderItemDao;

        public OrderService()
        {
            _orderDao = new OrderDao();
            _orderItemDao = new OrderItemDao();
        }

        public Dictionary<int, (string BarStatus, string KitchenStatus)> GetTableLocationPhases()
            => _orderDao.GetTableLocationPhases();

        public int CreateOrder(Order order)
        {
            int orderId = _orderDao.CreateOrder(order);
            foreach (var item in order.Items)
            {
                item.OrderId = orderId;
                _orderItemDao.CreateOrderItem(item);
            }
            return orderId;
        }
        public Order GetOrdersForAlreadyOrderedTable(int tableId)
        {
            return _orderDao.GetOrdersForAlreadyOrderedTable(tableId);
        }
        
        // get a list of orders item that is ready
        public List<OrderItem> GetReadyItemsByTableId(int tableId)
        {
            return _orderItemDao.GetReadyItemsByTableId(tableId);
        }

        //mark the ready items as served

        public void MarkAllReadyServedByTableId(int tableId)
        {
            _orderItemDao.MarkAllReadyServedByTableId(tableId);
        }

        //check if the there are no running order 
        public bool HasNoRunningItems(int tableId)
        {
            return _orderItemDao.CountRunningItemsByTableId(tableId) == 0;
        }
        public void SetOrderCreated(int orderId, bool isCreated)
        {
            _orderDao.SetOrderCreated(orderId, isCreated);
        }
    }
}
