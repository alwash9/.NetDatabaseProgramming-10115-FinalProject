using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Logging;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Control;

namespace NorthwindDB_Console_Final.Menus
{
    class EditProductsMenu
    {
        public void Start()
        {
            do
            {
                Console.WriteLine("\tEDIT PRODUCTS\n");

                Console.WriteLine("(1) Display all products *Doesn't work");
                Console.WriteLine("(2) Search for a specific product to edit (By the Product Name)");
                Console.WriteLine("(3) Find product to edit by Product ID");

                Console.WriteLine("Press ESC to go back");

                ConsoleKeyInfo keypress = Console.ReadKey();
                Console.WriteLine();

                NLogger logging = new NLogger();
                DisplayMenu dm = new DisplayMenu();

                if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
                {
                    NorthwindContext db = new NorthwindContext();
                    Console.WriteLine("Which product are you looking for?");

                    var results = db.SearchProductsByName(Console.ReadLine());
                    var rList = results.ToList();



                    if (results.Count() == 0)
                    {
                        logging.Log("WARN", "No results found. Try Again.");
                    }
                    else if (results.Count() == 1)
                    {
                        dm.DisplayProducts_Short(rList);

                        Console.WriteLine("Is this the correct Product?");
                        Console.WriteLine("If yes, Press Y. If No, Press N.");

                        do
                        {

                            ConsoleKeyInfo keypress2 = Console.ReadKey();
                            Console.WriteLine();

                            if (keypress2.Key == ConsoleKey.Y)
                            {
                                int pickID = rList[0].ProductID;
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
                        InputValidation iv = new InputValidation();
                        db.DisplayProducts_Short(rList);


                        Console.WriteLine("Which Product would you like to edit?");
                        Console.Write("Enter the Row number: \t");

                        string choice = Console.ReadLine();
                        int vInput = iv.IntValidation(choice);

                        int findID = rList[vInput - 1].ProductID;
                        Product pick = results.FirstOrDefault(p => p.ProductID == findID);

                        EditProduct(pick);

                    }

                }
                else if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
                {
                    NorthwindContext db = new NorthwindContext();
                    Console.WriteLine("What is the ID number of the product you are looking for?");
                    string check = Console.ReadLine();
                    InputValidation iv = new InputValidation();
                    int toFind = iv.IntValidation(check);


                    var results = db.Products.Where(p => p.ProductID == toFind);
                    var rList = results.ToList();

                    if (results.Count() == 0)
                    {
                        logging.Log("WARN", "No results found. Try Again.");
                    }
                    else if (results.Count() == 1)
                    {
                        dm.DisplayProducts_Short(rList);

                        Console.WriteLine("Is this the correct Product?");
                        Console.WriteLine("If yes, Press Y. If No, Press N.");

                        do
                        {

                            ConsoleKeyInfo keypress2 = Console.ReadKey();
                            Console.WriteLine();

                            if (keypress2.Key == ConsoleKey.Y)
                            {

                                EditProduct(rList[0]);
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

                        dm.DisplayProducts_Short(rList);


                        Console.WriteLine("Which Product would you like to edit?");
                        Console.Write("Enter the Row number: \t");

                        string choice = Console.ReadLine();
                        int vInput = iv.IntValidation(choice);


                        EditProduct(rList[vInput - 1]);

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
            ProductsAttr pa = new ProductsAttr();
            ConsoleKeyInfo keyPress;
            var change = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

            do
            {

                Console.WriteLine("Which part of the product would you like to edit?");

                Console.WriteLine("(1) ProductName");
                Console.WriteLine("(2) QuantityPerUnit");
                Console.WriteLine("(3) UnitPrice");
                Console.WriteLine("(4) UnitsInStock");
                Console.WriteLine("(5) UnitsOnOrder");
                Console.WriteLine("(6) ReorderLevel");
                Console.WriteLine("(7) Discontinued");

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

            } while (true);


        }

    }
}
