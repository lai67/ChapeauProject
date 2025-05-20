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
        private TableDao tableDao;
        public TableService()
        {
            tableDao = new TableDao();
        }
        public List<Table> GetAllTables()
        {
            return tableDao.GetAllTables();
        }
        
    }
}
