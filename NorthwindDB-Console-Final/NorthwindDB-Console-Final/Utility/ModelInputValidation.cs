using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Logging;
using NorthwindDB_Console_Final.Menus;
using NorthwindDB_Console_Final.Models;


namespace NorthwindDB_Console_Final.Utility
{
    //Made to improve readability and reduce clutter in the edit classes
    class ModelInputValidation : ValidationOptions
    {
        //private string nullableMessage = "\n\t* Note: This field allows null. Press nothing but ENTER to leave blank.";
        private NLogger logging = new NLogger();


        //PRODUCTS



        public string ProductName_Input()
        {
           
            Console.WriteLine("Please enter the product's name.");
            string productName = Console.ReadLine();

            return productName;
        }



        public string QuantityPerUnit_Input()
        {
            Console.WriteLine("What is the quantity/unit for this product.");
            string quantityPerUnit = Console.ReadLine();

            return quantityPerUnit;
        }

        public decimal? UnitPrice_Input()
        {
            decimal? unitPrice;

            do
            {
                Console.WriteLine("What is the unit price for this product?" + nullableMessage);
                string tempn = Console.ReadLine();

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

            return unitPrice;
        }

        public Int16? UnitsInStock_Input()
        {
            Int16? unitsInStock;

            do
            {
                Console.WriteLine("What is the number of units in stock?" + nullableMessage);
                string tempn = Console.ReadLine();

                if (ValidateInt16(tempn))
                {
                    if (tempn == "")
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

            return unitsInStock;
        }

        public Int16? UnitsOnOrder_Input()
        {
            Int16? unitsOnOrder;

            do
            {
                Console.WriteLine("What is the number of units on order?" + nullableMessage);
                string tempn = Console.ReadLine();
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

            return unitsOnOrder;
        }

        public Int16? ReorderLevel_Input()
        {
            
            Int16? reorderLevel;

            do
            {
                Console.WriteLine("What is the reorder level?" + nullableMessage);
                string tempn = Console.ReadLine();
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

            return reorderLevel;
        }

        public bool Discontinued_Input()
        {
            bool discontinued;

            do
            {
                Console.WriteLine("Is this product discontinued?");
                Console.WriteLine("If yes, Press Y. If No, Press N.");
                ConsoleKeyInfo keypress = Console.ReadKey();

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

            return discontinued;
        }

        public int? Category_Input()
        {
            NorthwindContext db = new NorthwindContext();
            DisplayOptions disOp = new DisplayOptions();


            Console.WriteLine("What category does this product belong to?" + nullableMessage);

            string tempn = Console.ReadLine();
            var category = db.Categories.FirstOrDefault(c => c.CategoryName.Contains(tempn));

            disOp.DisplayCategory(category);

            do
            {
                Console.WriteLine("Is this the correct category? (If yes, press Y. If no, press N.)");
                var keypress = Console.ReadKey();

                if (keypress.Key == ConsoleKey.Y)
                {

                    return category.CategoryId;
                }
                else if (keypress.Key == ConsoleKey.N)
                {
                    logging.Log("WARN", "Setting Category to Null");

                    return null;
                }
                else
                {
                    logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                }



            } while (true);

        }


        //CATEGORIES

        public string CategoryName_Input()
        {
            Console.WriteLine("Please enter the category's name.");
            string categoryName = Console.ReadLine();

            return categoryName;
        }

        public string CategoryDescription_Input()
        {
            Console.WriteLine("Please enter the category's description.");
            string categoryDescription = Console.ReadLine();

            return categoryDescription;
        }
    }
}

