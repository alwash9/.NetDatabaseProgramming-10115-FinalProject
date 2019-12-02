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
            Console.WriteLine("\tEDIT PRODUCT\n");

            Console.WriteLine("(1) Display all products");
            Console.WriteLine("(2) Search for a specific product to edit");

            ConsoleKeyInfo keypress = Console.ReadKey();

            NLogger logging = new NLogger();

            if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
            {
                NorthwindContext db = new NorthwindContext();
                Console.WriteLine("Which product are you looking for?");

                var results = db.SearchProducts(Console.ReadLine());
                var rList = results.ToList();



                if (results.Count() == 0)
                {
                    logging.Log("WARN", "No results found. Try Again.");
                }
                else if (results.Count() == 1)
                {
                    db.DisplayProducts_Short(rList);

                    Console.WriteLine("Is this the correct Product?");
                    Console.WriteLine("If yes, Press Y. If No, Press N.");

                    do
                    {

                        ConsoleKeyInfo keypress2 = Console.ReadKey();

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
                    //finish

                    Console.WriteLine("Which Product would you like to edit?");
                    Console.Write("Enter the Row number: \t");

                    string choice = Console.ReadLine();
                    int vInput = iv.IntValidation(choice);

                    Product pick = results.FirstOrDefault(p => p.ProductID == rList[vInput].ProductID);

                    EditProduct(pick); 
                    
                }

            }
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

                //Finish

                keyPress = Console.ReadKey();

                if (keyPress.Key == ConsoleKey.D0 || keyPress.Key == ConsoleKey.NumPad0)
                {
                    
                    db.Save();
                    break;
                }

                if (keyPress.Key == ConsoleKey.D1 || keyPress.Key == ConsoleKey.NumPad1)
                {
                    product.ProductName = pa.ProductName_Input();
                }

                if (keyPress.Key == ConsoleKey.D2 || keyPress.Key == ConsoleKey.NumPad2)
                {
                    product.QuantityPerUnit = pa.QuantityPerUnit_Input();
                }

                if (keyPress.Key == ConsoleKey.D3 || keyPress.Key == ConsoleKey.NumPad3)
                {
                    change.UnitPrice = pa.UnitPrice_Input();
                }

                if (keyPress.Key == ConsoleKey.D4 || keyPress.Key == ConsoleKey.NumPad4)
                {
                    product.UnitsInStock = pa.UnitsInStock_Input();
                }

                if (keyPress.Key == ConsoleKey.D5 || keyPress.Key == ConsoleKey.NumPad5)
                {
                    product.UnitsOnOrder = pa.UnitsOnOrder_Input();
                }

                if (keyPress.Key == ConsoleKey.D6 || keyPress.Key == ConsoleKey.NumPad6)
                {
                    product.ReorderLevel = pa.ReorderLevel_Input();
                }

                if (keyPress.Key == ConsoleKey.D7 || keyPress.Key == ConsoleKey.NumPad7)
                {
                    product.Discontinued = pa.Discontinued_Input();
                }

            } while (true);


        }

    }
}
