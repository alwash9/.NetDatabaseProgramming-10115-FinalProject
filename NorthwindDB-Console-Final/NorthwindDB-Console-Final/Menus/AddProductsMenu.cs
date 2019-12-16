using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Utility;
using NorthwindDB_Console_Final.Logging;
using System.Reflection;

namespace NorthwindDB_Console_Final.Menus
{
    class AddProductsMenu : ValidationOptions, IMenu
    {
        public void Start()
        {

            Console.WriteLine("\n\tADD A PRODUCT\n");


            string productName;
            string quantityPerUnit;
            decimal? unitPrice;
            Int16? unitsInStock;
            Int16? unitsOnOrder;
            Int16? reorderLevel;
            bool discontinued;
            int? categoryId;

            string tempn;
            NLogger logging = new NLogger();
            //string nullableMessage = "\n\t* Note: This field allows null. Press nothing but ENTER to leave blank.";

            Console.WriteLine("Please enter the product's name. (Required)");
            productName = Console.ReadLine();

            Console.WriteLine("What is the quantity/unit for this product.");
            quantityPerUnit = Console.ReadLine();

            do
            {
                Console.WriteLine("What is the unit price for this product?" + nullableMessage);
                tempn = Console.ReadLine();
                if (ValidateDecimal(tempn))
                {
                    if (tempn == "")
                    {
                        unitPrice = null;
                    }
                    else
                    {
                        unitPrice = decimal.Parse(tempn);                        
                    }
                    break;
                }
                else
                {
                    logging.Log("ERROR", "A proper decimal was not entered for the new product's unit price. Please try again.");

                }
            } while (true);

            do
            {
                Console.WriteLine("What is the number of units in stock?" + nullableMessage);
                tempn = Console.ReadLine();
                if (ValidateInt16(tempn))
                {
                    if(tempn == "")
                    {
                        unitsInStock = null;
                    }
                    else
                    {
                        unitsInStock = Int16.Parse(tempn);
                    }
                    break;
                }
                else
                {
                    logging.Log("ERROR", "A proper int (int16, smallint) was not entered for the new product's in stock number. Please try again.");

                }
            } while (true);

            do
            {
                Console.WriteLine("What is the number of units on order?" + nullableMessage);
                tempn = Console.ReadLine();
                if (ValidateInt16(tempn))
                {
                    if (tempn == "")
                    {
                        unitsOnOrder = null;
                    }
                    else
                    {
                        unitsOnOrder = Int16.Parse(tempn);
                    }
                    break;
                }
                else
                {
                    logging.Log("ERROR", "A proper int (int16, smallint) was not entered for the new product's units on order. Please try again.");

                }
            } while (true);


            do
            {
                Console.WriteLine("What is the reorder level?" + nullableMessage);
                tempn = Console.ReadLine();
                if (ValidateInt16(tempn))
                {
                    if (tempn == "")
                    {
                        reorderLevel = null;
                    }
                    else
                    {
                        reorderLevel = Int16.Parse(tempn);
                    }   
                    break;
                }
                else
                {
                    logging.Log("ERROR", "A proper int (int16, smallint) was not entered for the new product's reorder level. Please try again.");
                }
            } while (true);

            do
            {
                Console.WriteLine("Is this product discontinued?");
                Console.WriteLine("If yes, Press Y. If No, Press N.");
                ConsoleKeyInfo keypress = Console.ReadKey();
                Console.WriteLine();

                if (keypress.Key == ConsoleKey.Y)
                {
                    discontinued = true;
                    break;
                }
                else if (keypress.Key == ConsoleKey.N)
                {
                    discontinued = false;
                    break;
                }
                else
                {
                    logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");

                }
            } while (true);

            do
            {
                Console.WriteLine("What category does this product belong to?" + nullableMessage);

                tempn = Console.ReadLine();

                if (tempn == "" || tempn == null)
                {
                    categoryId = null;
                    break;
                }
                else
                {


                    NorthwindContext db = new NorthwindContext();
                    DisplayOptions disOp = new DisplayOptions();

                    var category = db.Categories.FirstOrDefault(c => c.CategoryName.Contains(tempn));

                    disOp.DisplayCategory(category);


                    Console.WriteLine("Is this the correct category? (If yes, press Y. If no, press N.)");
                    var keypress = Console.ReadKey();


                    if (keypress.Key == ConsoleKey.Y)
                    {

                        categoryId = category.CategoryId;
                        break;
                    }
                    else if (keypress.Key == ConsoleKey.N)
                    {
                        logging.Log("WARN", "Setting Category to Null");

                        categoryId = null;
                        break;
                    }
                    else
                    {
                        logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                    }
                }

            } while (true);


            //add category

            Product product = new Product
            {
                ProductName = productName,
                QuantityPerUnit = quantityPerUnit,
                UnitPrice = unitPrice,
                UnitsInStock = unitsInStock,
                UnitsOnOrder = unitsOnOrder,
                ReorderLevel = reorderLevel,
                Discontinued = discontinued,
                CategoryId = categoryId
            };

            if (ConfirmSelections(product) == true)
            {
                NorthwindContext db = new NorthwindContext();

                db.AddProduct(product);
            }
            else
            {
                logging.Log("INFO", "Operation Cancelled.");
                
            }
            


        }



    }
}
