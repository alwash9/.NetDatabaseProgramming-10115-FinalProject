using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Utility;
using NorthwindDB_Console_Final.Logging;

namespace NorthwindDB_Console_Final.Menus
{
    class AddCategory : ValidationOptions, IMenu
    {
        public void Start()
        {

            Console.WriteLine("\tADD CATEGORY\n");

            string categoryName;
            string description;

            NLogger logging = new NLogger();

            Console.WriteLine("Please enter the new Category name. (Required)");
            categoryName = StringValidation(Console.ReadLine());

            Console.WriteLine("What is the description of the new category?");
            description = StringValidation(Console.ReadLine());


            Category category = new Category
            {
                CategoryName = categoryName,
                Description = description
                
            };

            if (ConfirmSelections(category) == true)
            {
                NorthwindContext db = new NorthwindContext();
                db.AddCategory(category);

            }
            else
            {
                logging.Log("INFO", "Operation Cancelled.");
            }


        }


    }
}
