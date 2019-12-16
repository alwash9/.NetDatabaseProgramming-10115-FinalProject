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
            do
            {
                Console.WriteLine("\n\tCATEGORIES\n");

                Console.WriteLine("(1) Add new Categories");
                Console.WriteLine("(2) Edit an existing Category");
                Console.WriteLine("(3) Delete an existing Category");
                Console.WriteLine("(4) Display Categories");

                Console.WriteLine("(ESC) Return to Main menu");

                var keypress = Console.ReadKey();
                Console.WriteLine("");

                if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                {
                    IMenu aMenu = new AddCategory();
                    aMenu.Start();
                }
                else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
                {
                    IMenu eMenu = new EditCategoryMenu();
                    eMenu.Start();
                }
                else if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
                {
                    IMenu deMenu = new DeleteCategoriesMenu();
                    deMenu.Start();
                }
                else if (keypress.Key == ConsoleKey.D4 || keypress.Key == ConsoleKey.NumPad4)
                {
                    IMenu diMenu = new DisplayCategoryMenu();
                    diMenu.Start();
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