using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace Service
{
    public class TableService
    {
        private readonly TableDao tableDao;
        public TableService()
        {
            tableDao = new TableDao();
        }
        public List<Table> GetAllTables()
        {
            return tableDao.GetAllTables();
        }
        public Table GetTableById(int tableId)
        {
            return tableDao.GetTableById(tableId);
        }
        public void UpdateTableStatus(int tableId, TableStatus status)
        {
            tableDao.UpdateTableStatus(tableId, status);
        }
        
    }
}
