using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Control;
using NorthwindDB_Console_Final.Logging;


namespace NorthwindDB_Console_Final.Menus
{
    class TopMenu
    {
        private NLogger logging = new NLogger();

        public void Start()
        {
            Console.WriteLine("\t\t WELCOME!");

            do
            {

                Console.WriteLine("\tPlease choose an option. (Press the key that is enclosed by '()')\n");


                Console.WriteLine("(1) PRODUCTS MENU");
                Console.WriteLine("(0) TESTING");
                Console.WriteLine("(ESC) Exit");


                var keypress = Console.ReadKey();
                Console.WriteLine("\n");

                if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                {
                    ProductsMenu pMenu = new ProductsMenu();
                    pMenu.Start();
                }

                if (keypress.Key == ConsoleKey.D0 || keypress.Key == ConsoleKey.NumPad0)
                {
                    TestingClass testMenu = new TestingClass();
                    testMenu.Start();
                }



                else if (keypress.Key == ConsoleKey.Escape)
                {
                    logging.Log("WARN","Are you sure that you would like to quit the application?");
                    Console.WriteLine("Press the \"Y\" key if you'd truly like to exit, otherwise press any other key to continue.");

                    ConsoleKeyInfo end = Console.ReadKey();
                    Console.WriteLine();

                    if (end.Key == ConsoleKey.Y)
                    {
                        break;
                    }

                }
                else
                {
                    
                    logging.Log("WARN", "Please press a valid option. Try again.");
                }
            } while (true);
        }
    }
}
