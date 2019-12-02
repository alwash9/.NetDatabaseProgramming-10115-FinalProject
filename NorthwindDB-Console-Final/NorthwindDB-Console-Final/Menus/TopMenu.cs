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

        public void Start()
        {
            Console.WriteLine("(1) Add new Product");
            Console.WriteLine("(2) Edit an existing product");

            var keypress = Console.ReadKey();
            Console.WriteLine();

            if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
            {
                AddProductsMenu pMenu = new AddProductsMenu();
                pMenu.Start();
            }
            if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
            {
                EditProductsMenu eMenu = new EditProductsMenu();
                eMenu.Start();
            }
        }
    }
}
