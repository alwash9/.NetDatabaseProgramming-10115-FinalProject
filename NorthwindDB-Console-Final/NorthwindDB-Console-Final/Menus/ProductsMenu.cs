using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Logging;

namespace NorthwindDB_Console_Final.Menus
{
    class ProductsMenu
    {
        private NLogger logging = new NLogger();

        public void Start()
        {
            Console.WriteLine("(1) Add new Product");
            Console.WriteLine("(2) Edit an existing Product");
            Console.WriteLine("(3) Display Products");

            var keypress = Console.ReadKey();
            Console.WriteLine("");

            if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
            {
                AddProductsMenu pMenu = new AddProductsMenu();
                pMenu.Start();
            }
            else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
            {
                EditProductsMenu eMenu = new EditProductsMenu();
                eMenu.Start();
            }
            else if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
            {
                IMenu dmenu = new DisplayMenu();
                dmenu.Start();
            }
            else
            {
                logging.Log("WARN", "Please press a valid option. Try again.");
            }


        }

    }
}
