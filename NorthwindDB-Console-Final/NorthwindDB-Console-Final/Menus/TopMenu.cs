using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Menus;

namespace NorthwindDB_Console_Final.Menus
{
    class TopMenu
    {

        public void StartMenu()
        {
            Console.WriteLine("(1) Add new Product");

            if (Console.ReadKey().Key == ConsoleKey.D1 || Console.ReadKey().Key == ConsoleKey.NumPad1)
            {
                ProductMenu pMenu = new ProductMenu();
                pMenu.StartMenu();
            }
        }
    }
}
