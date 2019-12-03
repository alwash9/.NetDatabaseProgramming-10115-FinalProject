using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Logging;
using System.Reflection;

namespace NorthwindDB_Console_Final.Menus
{
    class AddProductsMenu
    {
        public void Start()
        {
            string productName;
            string quantityPerUnit;
            decimal? unitPrice;
            Int16? unitsInStock;
            Int16? unitsOnOrder;
            Int16? reorderLevel;
            bool discontinued;

            string tempn;
            NLogger logging = new NLogger();
            string nullableMessage = "\n\t* Note: This field allows null. Press nothing but ENTER to leave blank.";

            Console.WriteLine("Please enter the product's name.");
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

            Product product = new Product
            {
                ProductName = productName,
                QuantityPerUnit = quantityPerUnit,
                UnitPrice = unitPrice,
                UnitsInStock = unitsInStock,
                UnitsOnOrder = unitsOnOrder,
                ReorderLevel = reorderLevel,
                Discontinued = discontinued
            };

            ConfirmSelections(product);

        }


        //public for the ProductsAttr class. Pending.
        public bool ValidateDecimal(string input)
        {
            if(input == "")
            {
                return true;
            }
            var check = decimal.TryParse(input, out decimal result);
            return check;
        }

        //public for the ProductsAttr class. Pending.
        public bool ValidateInt16(string input)
        {
            if (input == "")
            {
                return true;
            }
            var check = Int16.TryParse(input, out short result);
            return check;
        }

        private void ConfirmSelections(Product product)
        {
            NLogger logging = new NLogger();

            //loop through properties
            //foreach (PropertyInfo prop in product.GetType().GetProperties())
            //{
            //    Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(prop)); //sketchy, check
            //}

            Console.WriteLine("\nProduct Name: {0}",      product.ProductName);
            Console.WriteLine("Quantity Per Unit: {0}", product.QuantityPerUnit);
            Console.WriteLine("Unit Price: {0}",        product.UnitPrice);
            Console.WriteLine("Units In Stock: {0}",    product.UnitsInStock);
            Console.WriteLine("Units On Order: {0}",    product.UnitsOnOrder);
            Console.WriteLine("Reorder Level: {0}",     product.ReorderLevel);
            Console.WriteLine("Discontinued: {0}\n",      product.Discontinued);

            
            do
            {
                Console.WriteLine("Is this correct?");
                Console.WriteLine("If yes, Press Y. If No, Press N.");
                ConsoleKeyInfo keypress = Console.ReadKey();
                Console.WriteLine();
                NorthwindContext db = new NorthwindContext();


                if (keypress.Key == ConsoleKey.Y)
                {
                    db.AddProduct(product);
                    break;
                }
                else if (keypress.Key == ConsoleKey.N)
                {
                    logging.Log("WARN", "Operation Cancelled.");
                    break;
                }
                else
                {
                    logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                }
            } while (true);

            Console.ReadLine();
        }
    }
}
