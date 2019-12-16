using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Utility;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Logging;

namespace NorthwindDB_Console_Final.Menus
{
    class DisplayProductMenu : ValidationOptions, IMenu
    {
        private NLogger logging = new NLogger();
        

        public void Start()
        {
            do
            {
                Console.WriteLine("\n\tDISPLAY PRODUCTS\n");

                Console.WriteLine("(1) Display all products");
                Console.WriteLine("(2) Find product details");

                Console.WriteLine("(ESC) Return to Products menu");

                var keypress = Console.ReadKey();
                Console.WriteLine("");

                DisplayOptions disOp = new DisplayOptions();
                NorthwindContext db = new NorthwindContext();

                //Display all
                if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                {
                    Console.WriteLine("(1) Display all products");
                    Console.WriteLine("(2) Display all active products");
                    Console.WriteLine("(3) Display all discontinued products");

                    var type = Console.ReadKey();
                    Console.WriteLine("");
                    if (type.Key == ConsoleKey.D1 || type.Key == ConsoleKey.NumPad1)
                    {
                        Console.WriteLine("(1) Display products in short form");
                        Console.WriteLine("(2) Display products in long form");

                        var form = Console.ReadKey();
                        Console.WriteLine("");

                        if (form.Key == ConsoleKey.D1 || form.Key == ConsoleKey.NumPad1)
                        {
                            disOp.DisplayAllProducts_Short();

                        }
                        else if (form.Key == ConsoleKey.D2 || form.Key == ConsoleKey.NumPad2)
                        {
                            disOp.DisplayAllProducts_Long();
                        }
                        else
                        {
                            logging.Log("WARN", "Please press a valid option. Try again.");
                        }
                    }
                    else if (type.Key == ConsoleKey.D2 || type.Key == ConsoleKey.NumPad2)
                    {
                        Console.WriteLine("(1) Display products in short form");
                        Console.WriteLine("(2) Display products in long form");

                        var form = Console.ReadKey();
                        Console.WriteLine("");

                        if (form.Key == ConsoleKey.D1 || form.Key == ConsoleKey.NumPad1)
                        {
                            disOp.DisplayAllActiveProducts_Short();
                        }
                        else if (form.Key == ConsoleKey.D2 || form.Key == ConsoleKey.NumPad2)
                        {
                            disOp.DisplayAllActiveProducts_Long();
                        }
                        else
                        {
                            logging.Log("WARN", "Please press a valid option. Try again.");
                        }
                    }
                    else if (type.Key == ConsoleKey.D3 || type.Key == ConsoleKey.NumPad3)
                    {
                        Console.WriteLine("(1) Display products in short form");
                        Console.WriteLine("(2) Display products in long form");

                        var form = Console.ReadKey();
                        Console.WriteLine("");

                        if (form.Key == ConsoleKey.D1 || form.Key == ConsoleKey.NumPad1)
                        {
                            disOp.DisplayAllDiscontinuedProducts_Short();
                        }
                        else if (form.Key == ConsoleKey.D2 || form.Key == ConsoleKey.NumPad2)
                        {
                            disOp.DisplayAllDiscontinuedProducts_Long();
                        }
                        else
                        {
                            logging.Log("WARN", "Please press a valid option. Try again.");
                        }
                    }
                    else
                    {
                        logging.Log("WARN", "Please press a valid option. Try again.");
                    }

                }
                else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
                {
                    Console.WriteLine("Please enter the product that you are searching for. (Name)");
                    string search = Console.ReadLine();



                    var results = db.SearchProducts(search);

                    if (results.Count() == 0)
                    {

                    }
                    else
                    {
                        Console.WriteLine("(1) Display results in short form");
                        Console.WriteLine("(2) Display results in long form");

                        var form = Console.ReadKey();
                        Console.WriteLine("");

                        if (form.Key == ConsoleKey.D1 || form.Key == ConsoleKey.NumPad1)
                        {
                            disOp.DisplayProducts_Short(results);
                        }
                        else if (form.Key == ConsoleKey.D2 || form.Key == ConsoleKey.NumPad2)
                        {
                            disOp.DisplayProducts_Long(results);
                        }
                        else
                        {
                            logging.Log("WARN", "Please press a valid option. Try again.");
                        }
                    }

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
