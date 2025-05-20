using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Menu_Item_Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Item_Category { get; set; }

        public int Stock { get; set; }

        public double Vat { get; set; }      
        public decimal Price { get; set; }   

        public int Preperation_Time { get; set; }

        public int Menu_Id { get; set; }
    }

    public enum Item_Category
    {
        Starter,
        Main,
        Entremet,
        Dessert,
        SoftDrink,
        Beer,
        Wine,
        SpiritDrink,
        Coffee
    }
}
