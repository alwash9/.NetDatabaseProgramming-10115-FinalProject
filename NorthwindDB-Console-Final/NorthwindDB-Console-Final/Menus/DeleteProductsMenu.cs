using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Utility;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Logging;
using System.Data.Entity;

namespace NorthwindDB_Console_Final.Menus
{
    class DeleteProductsMenu : ValidationOptions, IMenu
    {
        public void Start()
        {
            Console.WriteLine("\tDELETE PRODUCT\n");

            Console.WriteLine("Delete which product?\n");
            Console.WriteLine("(1) Find by ID");
            Console.WriteLine("(2) Find by Name");

            var keypress = Console.ReadKey();
            Console.WriteLine("");

            NorthwindContext db = new NorthwindContext();
            DisplayOptions disOp = new DisplayOptions();

            List<Product> results = null;

            if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
            {

                Console.WriteLine("Please enter the ID of the product that you are searching for.");
                int search = IntValidation(Console.ReadLine());


                results = db.SearchProducts(search).ToList();

            }
            else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
            {

                Console.WriteLine("Please enter the Name of the product that you are searching for.");
                string search = Console.ReadLine();

                results = db.SearchProducts(search).ToList();
            }
            else
            {
                NLogger logging = new NLogger();

                logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");

            }


            if (results == null)
            {

            }
            else if (results.Count() == 0)
            {

            }
            else if (results.Count() == 1)
            {

                disOp.DisplayProducts_Long(results);

                this.Delete(results[0]);
            }
            else
            {
                disOp.DisplayProducts_Short(results);


                Console.WriteLine("Which Product?");
                Console.Write("Enter the Row number: \t");

                string choice = Console.ReadLine();
                int vInput = IntValidation(choice);


                this.Delete(results[vInput - 1]);

            }
        }





        private void Delete(Product product)
        {
            if(ConfirmSelections(product))
            {
                NorthwindContext db = new NorthwindContext();
                db.RemoveProduct(product);
                //var toDelete = db.Products.Attach(product);
                //db.Products.Remove(product);
                //db.Entry(product).State = EntityState.Deleted;
            }
            else
            {
                NLogger logging = new NLogger();

                logging.Log("INFO", "Operation Cancelled.");
            }
        }
    }
}
