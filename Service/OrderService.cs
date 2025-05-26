using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService
    {
        private readonly OrderDao _dao = new();
        public Dictionary<int, (string BarStatus, string KitchenStatus)> GetTableLocationPhases()
            => _dao.GetTableLocationPhases();
    }
}
