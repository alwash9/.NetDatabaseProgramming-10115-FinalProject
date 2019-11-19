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
    class ProductMenu
    {
        public void StartMenu()
        {
            string productName;
            string quantityPerUnit;
            decimal unitPrice;
            Int16 unitsInStock;
            Int16 unitsOnOrder;
            Int16 reorderLevel;
            bool discontinued;

            string tempn;
            NLogger logging = new NLogger();
            string nullableMessage = "\n\t* Note: This field allows null.Press nothing but ENTER to leave blank.";

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
                    unitPrice = decimal.Parse(tempn);
                    break;
                }
                else
                {
                    logging.Log("A proper decimal was not entered for the new product's unit price. Please try again.", "ERROR");

                }
            } while (true);

            do
            {
                Console.WriteLine("What is the number of units in stock?" + nullableMessage);
                tempn = Console.ReadLine();
                if (ValidateInt16(tempn))
                {
                    unitsInStock = Int16.Parse(tempn);
                    break;
                }
                else
                {
                    logging.Log("A proper int (int16, smallint) was not entered for the new product's in stock number. Please try again.", "ERROR");

                }
            } while (true);

            do
            {
                Console.WriteLine("What is the number of units on order?" + nullableMessage);
                tempn = Console.ReadLine();
                if (ValidateInt16(tempn))
                {
                    unitsOnOrder = Int16.Parse(tempn);
                    break;
                }
                else
                {
                    logging.Log("A proper int (int16, smallint) was not entered for the new product's units on order. Please try again.", "ERROR");

                }
            } while (true);


            do
            {
                Console.WriteLine("What is the reorder level?" + nullableMessage);
                tempn = Console.ReadLine();
                if (ValidateInt16(tempn))
                {
                    reorderLevel = Int16.Parse(tempn);
                    break;
                }
                else
                {
                    logging.Log("A proper int (int16, smallint) was not entered for the new product's reorder level. Please try again.", "ERROR");

                }
            } while (true);

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
                    logging.Log("A valid key was not pressed. Please press (Y)es or (N)o.", "ERROR");

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

        private bool ValidateDecimal(string input)
        {
            var check = decimal.TryParse(input, out decimal result);
            return check;
        }

        private bool ValidateInt16(string input)
        {
            var check = Int16.TryParse(input, out short result);
            return check;
        }

        private void ConfirmSelections(Product product)
        {
            //loop through properties
            //foreach (PropertyInfo prop in product.GetType().GetProperties())
            //{
            //    Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(prop)); //sketchy, check
            //}

            Console.WriteLine(product.ProductName);
            Console.ReadLine();
        }
    }
}
