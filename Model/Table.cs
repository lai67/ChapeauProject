using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Table
    {
        public int TableNumber {  get; set; }

        public TableStatus Status { get; set; }

        public Table()
        {
            
        }
        public Table(int tableNumber, TableStatus statuse)
        {
            this.TableNumber = tableNumber;
            this.Status = statuse;
        }
    }
}

public enum TableStatus
{
    Occupied, Free,Booked
}