using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Table
    {
        public int Number {  get; set; }

        public TableStatus Status { get; set; }

    }
}

public enum TableStatus
{
    Occupied, Free
}