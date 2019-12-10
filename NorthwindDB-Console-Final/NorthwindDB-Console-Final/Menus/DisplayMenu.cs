using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Logging;

namespace NorthwindDB_Console_Final.Menus
{
    public class DisplayMenu : IMenu
    {
        private NLogger logging = new NLogger();
        private NorthwindContext db = new NorthwindContext();

        public void Start()
        {
            Console.WriteLine("(1) Display all products");
            Console.WriteLine("(2) Find product details");

            var keypress = Console.ReadKey();
            Console.WriteLine("");

            if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
            {
                Console.WriteLine("(1) Display all products");
                Console.WriteLine("(2) Display all active products");
                Console.WriteLine("(3) Display all discontinued products");

                var type = Console.ReadKey();
                Console.WriteLine("");
                if (type.Key == ConsoleKey.D1 || type.Key == ConsoleKey.NumPad1)
                {
                    Console.WriteLine("(1) Display products in short form");
                    Console.WriteLine("(2) Display products in long form");

                    var form = Console.ReadKey();
                    Console.WriteLine("");

                    if (form.Key == ConsoleKey.D1 || form.Key == ConsoleKey.NumPad1)
                    {
                        DisplayAllProducts_Short();
                    }
                    else if (form.Key == ConsoleKey.D2 || form.Key == ConsoleKey.NumPad2)
                    {
                        DisplayAllProducts_Long();
                    }
                    else
                    {
                        logging.Log("WARN", "Please press a valid option. Try again.");
                    }
                }
                else if (type.Key == ConsoleKey.D2 || type.Key == ConsoleKey.NumPad2)
                {
                    Console.WriteLine("(1) Display products in short form");
                    Console.WriteLine("(2) Display products in long form");

                    var form = Console.ReadKey();
                    Console.WriteLine("");

                    if (form.Key == ConsoleKey.D1 || form.Key == ConsoleKey.NumPad1)
                    {
                        DisplayAllActiveProducts_Short();
                    }
                    else if (form.Key == ConsoleKey.D2 || form.Key == ConsoleKey.NumPad2)
                    {
                        DisplayAllActiveProducts_Long();
                    }
                    else
                    {
                        logging.Log("WARN", "Please press a valid option. Try again.");
                    }
                }
                else if (type.Key == ConsoleKey.D3 || type.Key == ConsoleKey.NumPad3)
                {
                    Console.WriteLine("(1) Display products in short form");
                    Console.WriteLine("(2) Display products in long form");

                    var form = Console.ReadKey();
                    Console.WriteLine("");

                    if (form.Key == ConsoleKey.D1 || form.Key == ConsoleKey.NumPad1)
                    {
                        DisplayAllDiscontinuedProducts_Short();
                    }
                    else if (form.Key == ConsoleKey.D2 || form.Key == ConsoleKey.NumPad2)
                    {
                        DisplayAllDiscontinuedProducts_Long();
                    }
                    else
                    {
                        logging.Log("WARN", "Please press a valid option. Try again.");
                    }
                }
                else
                {
                    logging.Log("WARN", "Please press a valid option. Try again.");
                }

            }
            else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
            {
                Console.WriteLine("Please enter the product that you are searching for.");
                string search = Console.ReadLine();


                var results = SearchProducts(search);

                if(results.Count() == 0)
                {

                }
                else
                {
                    Console.WriteLine("(1) Display results in short form");
                    Console.WriteLine("(2) Display results in long form");

                    var form = Console.ReadKey();
                    Console.WriteLine("");

                    if (form.Key == ConsoleKey.D1 || form.Key == ConsoleKey.NumPad1)
                    {
                        DisplayProducts_Short(results);
                    }
                    else if (form.Key == ConsoleKey.D2 || form.Key == ConsoleKey.NumPad2)
                    {
                        DisplayProducts_Long(results);
                    }
                    else
                    {
                        logging.Log("WARN", "Please press a valid option. Try again.");
                    }
                }

            }
            else
            {
                logging.Log("WARN", "Please press a valid option. Try again.");
            }
        }


        private List<Product> SearchProducts(string search)
        {
            NLogger logging = new NLogger();
            var searchResult = db.Products.Where(b => b.ProductName.Contains(search)).ToList();

            if (searchResult.Count() == 0)
            {

                logging.Log("WARN", "There were no products found.");

            }
            else
            {
                logging.Log("INFO", searchResult.Count() + " Products Found.");
            }
            return searchResult;

        }


        //Returns possibly multiple search results. (QueryableList)
        public IQueryable<Product> SearchProductsDisplay(string searchName)
        {
            NLogger logging = new NLogger();
            var searchResult = db.Products.Where(b => b.ProductName.Contains(searchName));

            if (searchResult.Count() == 0)
            {

                logging.Log("WARN", "There were no products found.");

            }
            else
            {
                Console.WriteLine($"{"Product ID",-10}Product Name\n");
                foreach (var item in searchResult)
                {
                    Console.WriteLine($"{item.ProductID,-10}{item.ProductName}\n");
                }

            }

            return searchResult;
        }



        //Displays a short format of a passed in list of products. 
        public void DisplayProducts_Short(List<Product> products)
        {
            int Row = 0;

            if (products.Count() > 1)
            {
                Console.Write($"\n{"Row",-20}");
            }

            this.ProductDisplayShortFormatHeadingTemplate();

            foreach (var product in products)
            {
                if (products.Count() > 1)
                {
                    Console.Write($"{++Row,-20}");
                }
                
                this.ProductDisplayShortFormatTemplate(product);
                Console.WriteLine("");
            }
        }

        //Displays a short format of a passed in QueryableList of products.
        public void DisplayProducts_Short(IQueryable<Product> products)
        {
            this.ProductDisplayShortFormatHeadingTemplate();

            foreach (var product in products)
            {
                this.ProductDisplayShortFormatTemplate(product);
            }
        }
        

        //Displays a long format of a passed in list of products.
        public void DisplayProducts_Long(List<Product> products)
        {
            int Row = 0;
            foreach (var product in products)
            {
                if (products.Count() > 1)
                {
                    Console.WriteLine($"\n{"Row:",-20}{++Row}");
                }
                this.ProductDisplayLongFormatTemplate(product);
                Console.WriteLine($"{"Discontinued:",-25}{product.Discontinued}\n");
            }
        }

        //Displays a long format of a passed in Queryable list of products.
        public void DisplayProducts_Long(IQueryable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine();
                this.ProductDisplayLongFormatTemplate(product);
                Console.WriteLine($"{"Discontinued:",-25}{product.Discontinued}\n");
            }
        }


        //Display a short form of all products in the database.
        public void DisplayAllProducts_Short()
        {
            var allP = db.Products.OrderBy(b => b.ProductID);

            this.ProductDisplayShortFormatHeadingTemplate();

            foreach (var product in allP)
            {
                this.ProductDisplayShortFormatTemplate(product);
            }
        }
        //Display a long form of all products in the database.
        public void DisplayAllProducts_Long()
        {
            var allP = db.Products.OrderBy(b => b.ProductID);

            foreach (var product in allP)
            {
                Console.WriteLine();
                this.ProductDisplayLongFormatTemplate(product);
                Console.WriteLine($"{"Discontinued",-25}{product.Discontinued}\n");
            }

        }


        //Display all active products (short form)
        public void DisplayAllActiveProducts_Short()
        {
            this.ProductDisplayShortFormatHeadingTemplate();
            var allP = db.Products.OrderBy(b => b.ProductID).Where(p => p.Discontinued == false);

            foreach (var product in allP)
            {
                Console.WriteLine();
                this.ProductDisplayShortFormatTemplate(product);
            }
        }
        //Display all active products (long form)
        public void DisplayAllActiveProducts_Long()
        {
            var allP = db.Products.OrderBy(b => b.ProductID).Where(p => p.Discontinued == false);

            foreach (var product in allP)
            {
                Console.WriteLine();
                this.ProductDisplayLongFormatTemplate(product);
                Console.WriteLine();
            }

        }
        //Display all discontinued products (short form)
        public void DisplayAllDiscontinuedProducts_Short()
        {
            this.ProductDisplayShortFormatHeadingTemplate();
            var allP = db.Products.OrderBy(b => b.ProductID).Where(p => p.Discontinued == true);

            foreach (var product in allP)
            {
                Console.WriteLine();
                this.ProductDisplayShortFormatTemplate(product);
            }
        }
        //Display all discontinued products (long form)
        public void DisplayAllDiscontinuedProducts_Long()
        {
            var allP = db.Products.OrderBy(b => b.ProductID).Where(p => p.Discontinued == true);

            foreach (var product in allP)
            {
                Console.WriteLine();
                this.ProductDisplayLongFormatTemplate(product);
                Console.WriteLine();
            }

        }


        //Template for short form headings
        private void ProductDisplayShortFormatHeadingTemplate()
        {
            Console.WriteLine($"{"Product ID",-20}{"Product Name",-20}\n");
        }
        //Template for short form display
        private void ProductDisplayShortFormatTemplate(Product product)
        {

            Console.Write($"{product.ProductID,-20}");
            Console.Write($"{product.ProductName,-20}\n");

        }
        //Template for long form display
        private void ProductDisplayLongFormatTemplate(Product product)
        {
            Console.WriteLine($"{"Product ID:",-25}{product.ProductID}");
            Console.WriteLine($"{"Product Name:",-25}{product.ProductName}");
            Console.WriteLine($"{"Quantity Per Unit:",-25}{product.QuantityPerUnit}");
            Console.WriteLine($"{"Unit Price:",-25}{product.UnitPrice}");
            Console.WriteLine($"{"Units In Stock:",-25}{product.UnitsInStock}");
            Console.WriteLine($"{"Units On Order:",-25}{product.UnitsOnOrder}");
            Console.WriteLine($"{"Reorder Level:",-25}{product.ReorderLevel}");
            
        }




    }
}
