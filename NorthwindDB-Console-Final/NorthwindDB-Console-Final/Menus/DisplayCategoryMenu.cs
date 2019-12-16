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
    class DisplayCategoryMenu : ValidationOptions, IMenu
    {
        private NLogger logging = new NLogger();
        private NorthwindContext db = new NorthwindContext();

        public void Start()
        {
            Console.WriteLine("(1) Display all Categories");
            Console.WriteLine("(2) Display Category with all related products");

            var keypress = Console.ReadKey();
            Console.WriteLine("");

            DisplayOptions disOp = new DisplayOptions();

            //Display all categories
            if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
            {
                disOp.DisplayAllCategories();
            }
            else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
            {
                Console.WriteLine("What Category are you looking for? \n");

                Console.WriteLine("(1) Search by ID");
                Console.WriteLine("(2) Search by Name");
                Console.WriteLine("(3) Search by Description");

                var keypress2 = Console.ReadKey();
                Console.WriteLine("");

                Category choice = null;
                List<Category> results = null;

                if (keypress2.Key == ConsoleKey.D1 || keypress2.Key == ConsoleKey.NumPad1)
                {
                    Console.WriteLine("What is the ID of the Category you want to display?");
                    int toFind = IntValidation(Console.ReadLine());

                    results = db.SearchCategory(toFind).ToList();
                }
                else if (keypress2.Key == ConsoleKey.D2 || keypress2.Key == ConsoleKey.NumPad2)
                {
                    Console.WriteLine("What is the Name of the Category you want to display?");
                    string toFind = Console.ReadLine();

                     results = db.SearchCategory(toFind, true).ToList();
                }
                else if (keypress2.Key == ConsoleKey.D3 || keypress2.Key == ConsoleKey.NumPad3)
                {
                    Console.WriteLine("What is the Description of the Category you want to display?");
                    string toFind = Console.ReadLine();

                    results = db.SearchCategory(toFind, false).ToList();
                }
                else
                {
                    logging.Log("WARN", "Please press a valid option. Try again.");
                }



                if (results == null)
                {

                }

                else if (results.Count == 0)
                {

                }
                else if (results.Count == 1)
                {
                    disOp.DisplayCategory(results[0]);
                    choice = results[0];
                }
                else
                {

                    int row = 0;

                    foreach (var category in results)
                    {
                        Console.WriteLine("Row: {0}", row++);
                        disOp.DisplayCategory(category);
                        Console.WriteLine("");
                    }


                    Console.WriteLine("Which Category would you like to display?");
                    Console.Write("Enter the Row number: \t");

                    string rowChoice = Console.ReadLine();
                    int vInput = IntValidation(rowChoice);


                    choice = results[vInput - 1];
                }



                Console.WriteLine("(1) Display all Products");
                Console.WriteLine("(2) Display only active Products");
                Console.WriteLine("(3) Display only discontinued Products");

                var keypress3 = Console.ReadKey();
                Console.WriteLine("");

                if (choice == null)
                {

                }

                else if (keypress3.Key == ConsoleKey.D1 || keypress3.Key == ConsoleKey.NumPad1)
                {
                    disOp.DisplayCategoryAndProducts(choice);

                }
                else if (keypress3.Key == ConsoleKey.D2 || keypress3.Key == ConsoleKey.NumPad2)
                {
                    disOp.DisplayCategoryAndActiveProducts(choice);
                }
                else if (keypress3.Key == ConsoleKey.D3 || keypress3.Key == ConsoleKey.NumPad3)
                {
                    disOp.DisplayCategoryAndDiscontinuedProducts(choice);
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
    }


}
