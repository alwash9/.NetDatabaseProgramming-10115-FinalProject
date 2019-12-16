using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Logging;

namespace NorthwindDB_Console_Final.Utility
{
    abstract class ValidationOptions
    {
        private NLogger logging = new NLogger();

        public string nullableMessage = "\n\t* Note: This field allows null. Press nothing but ENTER to leave blank.";


        protected int IntValidation(string input)
        {
            do
            {
                if (input == "" || input == null)
                {
                    logging.Log("WARN", "Input was null and will be replaced with 0.");
                    return 0;
                }
                else if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    logging.Log("ERROR", "A valid input was not entered! Please enter a valid Integer number.");
                    Console.Write("Input?:\t"); input = Console.ReadLine();

                }

            } while (true);

        }

        protected int? IntValidationNullable(string input)
        {
            do
            {
                if (input == "" || input == null)
                {
                    logging.Log("WARN", "Input string was empty or null. No further action required.");
                    return null;
                }
                else if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    logging.Log("ERROR", "A valid input was not entered! Please enter a valid Integer number.");
                    Console.Write("Input?:\t"); input = Console.ReadLine();

                }

            } while (true);

        }

        protected string StringValidation(string input)
        {
            do
            {
                if (input == "" || input == null)
                {
                    logging.Log("WARN", "Input was null and this is a required field. Please enter a valid name or description.");
                    Console.Write("Input?:\t");
                    input = Console.ReadLine();
                }
                else
                {
                    return input;
                }

            } while (true);
        }

        //Confirms selections for Products
        protected bool ConfirmSelections(Product product)
        {
            NLogger logging = new NLogger();

            //loop through properties
            //foreach (PropertyInfo prop in product.GetType().GetProperties())
            //{
            //    Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(prop)); //sketchy, check
            //}

            Console.WriteLine("\nProduct Name: {0}", product.ProductName);
            Console.WriteLine("Quantity Per Unit: {0}", product.QuantityPerUnit);
            Console.WriteLine("Unit Price: {0}", product.UnitPrice);
            Console.WriteLine("Units In Stock: {0}", product.UnitsInStock);
            Console.WriteLine("Units On Order: {0}", product.UnitsOnOrder);
            Console.WriteLine("Reorder Level: {0}", product.ReorderLevel);
            Console.WriteLine("Discontinued: {0}\n", product.Discontinued);


            do
            {
                Console.WriteLine("Is this correct?");
                Console.WriteLine("If yes, Press Y. If No, Press N.");
                ConsoleKeyInfo keypress = Console.ReadKey();
                Console.WriteLine();



                if (keypress.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (keypress.Key == ConsoleKey.N)
                {
                    
                    return false;
                }
                else
                {
                    logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                }

                keypress = Console.ReadKey();

            } while (true);

        }

        //Confirms selections for Categories
        protected bool ConfirmSelections(Category category)
        {


            Console.WriteLine("\nCategory Name: {0}", category.CategoryName);
            Console.WriteLine("Category Description: {0}\n", category.Description);


            do
            {
                Console.WriteLine("Is this correct?");
                Console.WriteLine("If yes, Press Y. If No, Press N.");
                ConsoleKeyInfo keypress = Console.ReadKey();
                Console.WriteLine();



                if (keypress.Key == ConsoleKey.Y)
                {

                    return true;
                }
                else if (keypress.Key == ConsoleKey.N)
                {
                    return false;
                }
                else
                {
                    logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                }

                keypress = Console.ReadKey();

            } while (true);

            
        }

    }
}
