using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Logging;

namespace NorthwindDB_Console_Final.Menus
{
    class DisplayMenu
    {
        private NLogger logging = new NLogger();
        private NorthwindContext db = new NorthwindContext();

        public void Start()
        { }

        //Returns possibly multiple search results. (QueryableList)
        public IQueryable<Product> SearchProducts(string searchName)
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
            Console.Write($"\n{"Row",-20}");

            this.ProductDisplayShortFormatHeadingTemplate();

            foreach (var product in products)
            {
                Console.Write($"{++Row,-20}");
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
                Console.WriteLine($"\n{"Row:",-20}{++Row}");
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
