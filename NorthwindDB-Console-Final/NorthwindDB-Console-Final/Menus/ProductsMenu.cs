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
            do
            {
                Console.WriteLine("\n\tPRODUCTS\n");

                Console.WriteLine("(1) Add new Product");
                Console.WriteLine("(2) Edit an existing Product");
                Console.WriteLine("(3) Delete an existing Product");
                Console.WriteLine("(4) Display Products");

                Console.WriteLine("(ESC) Return to Main menu");

                var keypress = Console.ReadKey();
                Console.WriteLine("");

                if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                {
                    IMenu aMenu = new AddProductsMenu();
                    aMenu.Start();
                }
                else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
                {
                    IMenu eMenu = new EditProductsMenu();
                    eMenu.Start();
                }
                else if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
                {
                    IMenu deMenu = new DeleteProductsMenu();
                    deMenu.Start();
                }
                else if (keypress.Key == ConsoleKey.D4 || keypress.Key == ConsoleKey.NumPad4)
                {
                    IMenu dimenu = new DisplayProductMenu();
                    dimenu.Start();
                }
                else if (keypress.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    logging.Log("WARN", "Please press a valid option. Try again.");
                }

            } while (true);

        }

    }
}
