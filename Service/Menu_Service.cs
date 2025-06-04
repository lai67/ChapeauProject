using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace Service
{
    public class Menu_Service
    {
        MenuItemDao menuItemDao;

        public Menu_Service()
        {
            menuItemDao = new MenuItemDao();
        }
        // This method is used to get all menus(Lunch, Dinner, Drinks)
        public List<MenuModel> GetAllMenus()
        {
            var menus = new List<MenuModel>();

            foreach (MenuType menuType in System.Enum.GetValues(typeof(MenuType)))
            {
                menus.Add(new MenuModel
                {
                    Id = (int)menuType,
                    Name = menuType.ToString()
                });
            }
            return menus;
        }
       
        public List<MenuItemModel> GetItemsByMenuAndCategory(int menuId,string menuCategory)
        {
            return menuItemDao.GetItemsByMenuAndCategory(menuId, menuCategory);
        }
        public List<MenuItemModel> GetItemsByMenuId(int menuId)
        {
            return menuItemDao.GetItemsByMenuId(menuId);
        }
        public void DecreaseMenuItemStock(int menuItemid,int count)
        {
            menuItemDao.DecreaseMenuItemStock(menuItemid, count);
        }

    }
}
