using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Logging;

namespace NorthwindDB_Console_Final.Utility
{
    class DisplayOptions
    {
         

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

        //Displays a long format of a passed in product
        public void DisplayProduct_Long(Product product)
        {
        
            this.ProductDisplayLongFormatTemplate(product);
            Console.WriteLine($"{"Discontinued:",-25}{product.Discontinued}\n");
            
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
            NorthwindContext db = new NorthwindContext();

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
            NorthwindContext db = new NorthwindContext();

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
            NorthwindContext db = new NorthwindContext();

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
            NorthwindContext db = new NorthwindContext();

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
            NorthwindContext db = new NorthwindContext();

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
            NorthwindContext db = new NorthwindContext();

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


        //CATEGORIES

        //Display all categories in the database.
        public void DisplayAllCategories()
        {
            NorthwindContext db = new NorthwindContext();

            var allC = db.Categories.OrderBy(b => b.CategoryId);

            this.CategoryDisplayHeadingTemplate();

            foreach (var category in allC)
            {
                this.CategoryDisplay(category);
            }
        }

        //Display from a list of categories
        public void DisplayCategories(List<Category> categories)
        {

            this.CategoryDisplayHeadingTemplate();

            foreach (var category in categories)
            {

                this.CategoryDisplay(category);
                Console.WriteLine("");
            }
        }

        //Display from a single category
        public void DisplayCategory(Category category)
        {
            Console.WriteLine("Category ID: {0}", category.CategoryId);
            Console.WriteLine("Category Name: {0}", category.CategoryName);
            Console.WriteLine("Category Description: {0}\n", category.Description);
        }

        //Display from a single category and its related products.
        public void DisplayCategoryAndProducts(Category category)
        {

            this.DisplayCategory(category);
            Console.WriteLine("\n--PRODUCTS--");
            this.DisplayProducts_Short(category.Products);

        }

        public void DisplayCategoryAndActiveProducts(Category category)
        {

            this.DisplayCategory(category);
            Console.WriteLine("\n--PRODUCTS--");
            this.DisplayProducts_Short(category.Products.Where(p => p.Discontinued == false).ToList());

        }

        public void DisplayCategoryAndDiscontinuedProducts(Category category)
        {

            this.DisplayCategory(category);
            Console.WriteLine("\n--PRODUCTS--");
            this.DisplayProducts_Short(category.Products.Where(p => p.Discontinued == true).ToList());

        }



        //Template for short form headings
        private void CategoryDisplayHeadingTemplate()
        {
            Console.WriteLine($"{"Category ID",-10}{"Category Name",-10}\n");
            
        }
        //Template for short form display
        private void CategoryDisplay(Category category)
        {

            Console.Write($"{category.CategoryId,-20}");
            Console.Write($"{category.CategoryName,-20}\n");
            Console.WriteLine("Category Description: {0}", category.Description);

        }






    }
}
