using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Logging;
using NorthwindDB_Console_Final.Utility;

namespace NorthwindDB_Console_Final.Menus
{
    class CategoriesMenu : ValidationOptions, IMenu
    {
        private NLogger logging = new NLogger();

        public void Start()
        {
            Console.WriteLine("(1) Add new Categories");
            Console.WriteLine("(2) Edit an existing Category");
            Console.WriteLine("(3) Display Categories");

            var keypress = Console.ReadKey();
            Console.WriteLine("");

            if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
            {
                AddProductsMenu pMenu = new AddProductsMenu();
                pMenu.Start();
            }
            else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
            {
                
            }
            else if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
            {
                
            }
            else
            {
                logging.Log("WARN", "Please press a valid option. Try again.");
            }


        }

    }
}