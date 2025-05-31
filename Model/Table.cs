using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Table
    {
        public int Id { get; set; } 
        public int TableNumber {  get; set; }
        public int tableId { get; set; }

        public TableStatus Status { get; set; }

        public Table()
        {
            
        }
        public Table(int tableId,int tableNumber, TableStatus statuse)
        {
            this.TableNumber = tableNumber;
            this.Status = statuse;
            this.tableId = tableId;
        }
    }

    public enum TableStatus
    {
        Occupied, Free, Booked
    }
}
