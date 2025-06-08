using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Model;
using System.Data;
using System.Data.SqlTypes;


namespace DAL
{
    public class MenuItemDao : BaseDao
    {
        // This method is used to get all items by menu and category
        public List<MenuItemModel> GetItemsByMenuAndCategory(int menuId, string itemCategory)
        {
            string query = @"
            SELECT mi.*
            FROM Menu_Item mi
            JOIN Menu_And_Items mai ON mi.id = mai.menu_item_id
            WHERE mai.menu_id = @menuId AND mi.item_category = @category";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@menuId", menuId),
        new SqlParameter("@category", itemCategory)
            };

            DataTable dt = ExecuteSelectQuery(query, parameters);
            return ConvertToList(dt);
        }

        // This method is used to get all items by menu id.
        public List<MenuItemModel> GetItemsByMenuId(int menuId)
        {
            string query = "SELECT * FROM Menu_Item WHERE menu_id = @menuId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@menuId", menuId)
            };
            DataTable dt = ExecuteSelectQuery(query, parameters);
            return ConvertToList(dt);
        }


        // This method is used to convert the DataTable to a List of Menu_Item_Model
        private List<MenuItemModel> ConvertToList(DataTable dataTable)
        {
            List<MenuItemModel> menuItemList = new List<MenuItemModel>();
            foreach (DataRow row in dataTable.Rows)
            {
                MenuItemModel menuItem = new MenuItemModel
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    Item_Category = row["item_category"].ToString(),
                    Stock = Convert.ToInt32(row["stock"]),
                    Vat = Convert.ToDecimal(row["vat"]),
                    Price = Convert.ToDecimal(row["price"]),
                    PreparationTime = Convert.ToInt32(row["preparation_time"]),
                    Menu_Id = row.Table.Columns.Contains("menu_id") && row["menu_id"] != DBNull.Value // in case if its null.
                          ? Convert.ToInt32(row["menu_id"])
                          : 0

                };
                menuItemList.Add(menuItem);
            }
            return menuItemList;
        }

        //Check menu item stock if there are enough items in stock.
        public int CheckMenuItemStock(int menuItemId)
        {
            string query = "SELECT stock FROM Menu_Item WHERE id = @menuItemId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@menuItemId", menuItemId)
            };
            DataTable dt = ExecuteSelectQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["stock"]);
            }
            return 0; // Return 0 if no item found
        }
        // Method to decrease the stock of a menu item.
        public void DecreaseMenuItemStock(int menuItemId, int count)
        {
            string query = "UPDATE Menu_Item SET stock = stock - @count WHERE id = @menuItemId";
            SqlParameter[] parameters = {
            new SqlParameter("@count", count),
            new SqlParameter("@menuItemId", menuItemId)
             };
            ExecuteEditQuery(query, parameters);
        }

        public MenuItemModel GetMenuItemById(int menuItemId)
        {
            string query = "SELECT * FROM Menu_Item WHERE id = @menuItemId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@menuItemId", menuItemId)
            };
            DataTable dt = ExecuteSelectQuery(query, parameters);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new MenuItemModel
            {
                Id = Convert.ToInt32(row["id"]),
                Name = row["name"].ToString(),
                Item_Category = row["item_category"].ToString(),
                Stock = Convert.ToInt32(row["stock"]),
                Vat = Convert.ToDouble(row["vat"]),
                Price = Convert.ToDecimal(row["price"]),
                PreparationTime = Convert.ToInt32(row["preparation_time"]),
                Menu_Id = row.Table.Columns.Contains("menu_id") && row["menu_id"] != DBNull.Value // in case if its null.
                          ? Convert.ToInt32(row["menu_id"])
                          : 0
            };
        }


    }
    




}
