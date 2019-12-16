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
    class DeleteCategoriesMenu : ValidationOptions, IMenu
    {
        public void Start()
        {
            Console.WriteLine("\tDELETE CATEGORY\n");

            Console.WriteLine("Delete which Category?\n");
            Console.WriteLine("(1) Find by ID");
            Console.WriteLine("(2) Find by Name");

            var keypress = Console.ReadKey();
            Console.WriteLine("");

            NorthwindContext db = new NorthwindContext();
            DisplayOptions disOp = new DisplayOptions();

            List<Category> results = null;

            if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
            {

                Console.WriteLine("Please enter the ID of the category that you are searching for.");
                int search = IntValidation(Console.ReadLine());


                results = db.SearchCategory(search).ToList();

            }
            else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
            {

                Console.WriteLine("Please enter the Name of the category that you are searching for.");
                string search = Console.ReadLine();

                results = db.SearchCategory(search, true).ToList();
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

                disOp.DisplayCategory(results[0]);

                this.Delete(results[0]);

            }
            else
            {
                int row = 0;

                foreach (var category in results)
                {
                    Console.WriteLine("Row: ", ++row);
                    disOp.DisplayCategory(category);
                }
                


                Console.WriteLine("Which Product?");
                Console.Write("Enter the Row number: \t");

                string choice = Console.ReadLine();
                int vInput = IntValidation(choice);


                this.Delete(results[vInput - 1]);

            }
        }





        private void Delete(Category category)
        {
            if (ConfirmSelections(category))
            {
                NorthwindContext db = new NorthwindContext();

                Console.WriteLine("Delete all related products?\n If yes, enter \"Yes\" otherwise, enter anything else for No. ");

                string del = Console.ReadLine();
                if(del.ToUpper() == "YES")
                {
                    
                    var prodDelete = category.Products;

                    foreach (var item in prodDelete)
                    {
                        db.RemoveProduct(item);
                    }
                }

                db.RemoveCategory(category);
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
