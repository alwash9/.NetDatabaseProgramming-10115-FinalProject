using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Logging;
using NorthwindDB_Console_Final.Utility;
using NorthwindDB_Console_Final.Models;


namespace NorthwindDB_Console_Final.Menus
{
    class EditProductsMenu : ValidationOptions, IMenu
    {
        public void Start()
        {
            do
            {
                Console.WriteLine("\n\tEDIT PRODUCTS\n");

                Console.WriteLine("(1) Display all products");
                Console.WriteLine("(2) Search for a specific product to edit by the Product Name");
                Console.WriteLine("(3) Find product to edit by Product ID");

                Console.WriteLine("Press ESC to go back");

                ConsoleKeyInfo keypress = Console.ReadKey();
                Console.WriteLine();

                NLogger logging = new NLogger();
                DisplayOptions disOp = new DisplayOptions();

                //Display all products in a short format
                if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                {
                    disOp.DisplayAllProducts_Short();
                }

                //Search for product to edit by name
                else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
                {
                    NorthwindContext db = new NorthwindContext();
                    Console.WriteLine("Which product are you looking for?");

                    var results = db.SearchProducts(Console.ReadLine());
                    



                    if (results.Count() == 0)
                    {
                        logging.Log("WARN", "No results found. Try Again.");
                    }
                    else if (results.Count() == 1)
                    {
                        disOp.DisplayProducts_Short(results);

                        Console.WriteLine("Is this the correct Product?");
                        Console.WriteLine("If yes, Press Y. If No, Press N.");

                        do
                        {

                            ConsoleKeyInfo keypress2 = Console.ReadKey();
                            Console.WriteLine();

                            if (keypress2.Key == ConsoleKey.Y)
                            {
                                int pickID = results[0].ProductID;
                                Product pick = results.FirstOrDefault(p => p.ProductID == pickID);
                                EditProduct(pick);
                                break;
                            }
                            else if (keypress2.Key == ConsoleKey.N)
                            {

                                break;
                            }
                            else
                            {
                                logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                            }

                        } while (true);
                    }
                    else
                    {


                        disOp.DisplayProducts_Short(results);


                        Console.WriteLine("Which Product would you like to edit?");
                        Console.Write("Enter the Row number: \t");

                        string choice = Console.ReadLine();
                        int vInput = IntValidation(choice);

                        int findID = results[vInput - 1].ProductID;
                        Product pick = results.FirstOrDefault(p => p.ProductID == findID);

                        EditProduct(pick);

                    }

                }
                //Find product to edit by ID
                else if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
                {
                    NorthwindContext db = new NorthwindContext();
                    Console.WriteLine("What is the ID number of the product you are looking for?");
                    string check = Console.ReadLine();
                    int toFind = IntValidation(check);


                    var results = db.Products.Where(p => p.ProductID == toFind).ToList();

                    if (results.Count() == 0)
                    {
                        logging.Log("WARN", "No results found. Try Again.");
                    }
                    else if (results.Count() == 1)
                    {
                        disOp.DisplayProducts_Short(results);

                        Console.WriteLine("Is this the correct Product?");
                        Console.WriteLine("If yes, Press Y. If No, Press N.");

                        do
                        {

                            ConsoleKeyInfo keypress2 = Console.ReadKey();
                            Console.WriteLine();

                            if (keypress2.Key == ConsoleKey.Y)
                            {

                                EditProduct(results[0]);
                                break;
                            }
                            else if (keypress2.Key == ConsoleKey.N)
                            {

                                break;
                            }
                            else
                            {
                                logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                            }

                        } while (true);
                    }
                    else
                    {

                        disOp.DisplayProducts_Short(results);


                        Console.WriteLine("Which Product would you like to edit?");
                        Console.Write("Enter the Row number: \t");

                        string choice = Console.ReadLine();
                        int vInput = IntValidation(choice);


                        EditProduct(results[vInput - 1]);

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

        private void EditProduct(Product product)
        {
            NorthwindContext db = new NorthwindContext();
            ModelInputValidation pa = new ModelInputValidation();
            ConsoleKeyInfo keyPress;
            var change = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

            do
            {

                Console.WriteLine("\nWhich part of the product would you like to edit?");

                Console.WriteLine("(1) ProductName");
                Console.WriteLine("(2) QuantityPerUnit");
                Console.WriteLine("(3) UnitPrice");
                Console.WriteLine("(4) UnitsInStock");
                Console.WriteLine("(5) UnitsOnOrder");
                Console.WriteLine("(6) ReorderLevel");
                Console.WriteLine("(7) Discontinued");
                Console.WriteLine("(8) Category");

                Console.WriteLine("When you are finish editing the product press (0) to save and go back.");


                keyPress = Console.ReadKey();
                Console.WriteLine();

                if (keyPress.Key == ConsoleKey.D0 || keyPress.Key == ConsoleKey.NumPad0)
                {
                    
                    db.Save();
                    break;
                }

                if (keyPress.Key == ConsoleKey.D1 || keyPress.Key == ConsoleKey.NumPad1)
                {
                    change.ProductName = pa.ProductName_Input();
                }

                if (keyPress.Key == ConsoleKey.D2 || keyPress.Key == ConsoleKey.NumPad2)
                {
                    change.QuantityPerUnit = pa.QuantityPerUnit_Input();
                }

                if (keyPress.Key == ConsoleKey.D3 || keyPress.Key == ConsoleKey.NumPad3)
                {
                    change.UnitPrice = pa.UnitPrice_Input();
                }

                if (keyPress.Key == ConsoleKey.D4 || keyPress.Key == ConsoleKey.NumPad4)
                {
                    change.UnitsInStock = pa.UnitsInStock_Input();
                }

                if (keyPress.Key == ConsoleKey.D5 || keyPress.Key == ConsoleKey.NumPad5)
                {
                    change.UnitsOnOrder = pa.UnitsOnOrder_Input();
                }

                if (keyPress.Key == ConsoleKey.D6 || keyPress.Key == ConsoleKey.NumPad6)
                {
                    change.ReorderLevel = pa.ReorderLevel_Input();
                }

                if (keyPress.Key == ConsoleKey.D7 || keyPress.Key == ConsoleKey.NumPad7)
                {
                    change.Discontinued = pa.Discontinued_Input();
                }

                if (keyPress.Key == ConsoleKey.D8 || keyPress.Key == ConsoleKey.NumPad8)
                {
                    change.CategoryId = pa.Category_Input();
                }

            } while (true);


        }

    }
}
