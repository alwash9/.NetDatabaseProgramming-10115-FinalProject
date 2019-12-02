using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using NorthwindDB_Console_Final.Logging;



namespace NorthwindDB_Console_Final.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("name=NorthwindContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


        public void Save()
        {
            NLogger logger = new NLogger();
            try
            {
                this.SaveChanges();

            }
            catch (Exception ex)
            {
                logger.Log("ERROR", "" + ex);
                throw;
            }

            logger.Log("INFO", "Changes were saved to the database.");
        }

        public void AddProduct (Product product)
        {
            NLogger logger = new NLogger();
            try
            {
                this.Products.Add(product);
                this.SaveChanges();
                logger.Log("INFO", "Product has been added.");
            }
            catch (Exception ex)
            {
                logger.Log("ERROR", "An error occurred when attempting to add a new product.\n" + ex);
                throw;
            }
            
        }

        public IQueryable<Product> SearchProducts(string searchName)
        {
            NLogger logging = new NLogger();
            var searchResult = this.Products.Where(b => b.ProductName.Contains(searchName));

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

        public void DisplayProducts_Short(List<Product> products)
        {
            Console.WriteLine($"{"Product ID", -20}{"Product Name", -20}\n");
            int Row = 0;

            foreach (var product in products)
            {
                Console.Write($"{++Row,-10}");
                Console.Write($"{product.ProductID, -20}");
                Console.Write($"{product.ProductName, -20}");
            }
        }

        public void DisplayProducts_Short(IQueryable<Product> products)
        {
            Console.WriteLine($"{"Product ID",-20}{"Product Name",-20}\n");

            foreach (var product in products)
            {
                Console.Write($"{product.ProductID,-20}");
                Console.Write($"{product.ProductName,-20}");
            }
        }

        public void DisplayAllProducts_Short()
        {
            var allP = this.Products.OrderBy(b => b.ProductID);

            Console.WriteLine($"{"Product ID",-20}{"Product Name",-20}\n");

            foreach (var product in allP)
            {
                Console.Write($"{product.ProductID,-20}");
                Console.Write($"{product.ProductName,-20}");
            }
        }

        public void DisplayAllProducts_Full()
        {
            var allP = this.Products.OrderBy(b => b.ProductID);

            foreach (var product in allP)
            {
                Console.WriteLine($"{"Product ID",          -25}{product.ProductID}");
                Console.WriteLine($"{"Product Name",        -25}{product.ProductName}");
                Console.WriteLine($"{"Quantity Per Unit",   -25}{product.QuantityPerUnit}");
                Console.WriteLine($"{"Unit Price",          -25}{product.UnitPrice}");
                Console.WriteLine($"{"Units In Stock",      -25}{product.UnitsInStock}");
                Console.WriteLine($"{"Units On Order",      -25}{product.UnitsOnOrder}");
                Console.WriteLine($"{"Reorder Level",       -25}{product.ReorderLevel}");
                Console.WriteLine($"{"Discontinued",        -25}{product.Discontinued}");
            }




        }



        public void AddCategory(Category category)
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            if(this.Validation(category))
            {
                logger.Info("Validation passed");

                this.Categories.Add(category);
                this.SaveChanges();
                logger.Info("Category Added");
            }
            else
            {
                Console.WriteLine("Please try again. Please ENTER to continue.");
                Console.ReadLine();
            }

        }

        public bool Validation(Category category)
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


            ValidationContext vContext = new ValidationContext(category, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(category, vContext, results, true);
            if (isValid)
            {
                // check for unique name
                if (this.Categories.Any(c => c.CategoryName.ToLower() == category.CategoryName.ToLower()))
                {
                    // generate validation error
                    isValid = false;
                    results.Add(new ValidationResult("Name exists", new string[] { "CategoryName" }));
                }

                if (category.Description.ToLower().Replace(" ", "") == category.CategoryName.ToLower().Replace(" ", ""))
                {
                    isValid = false;
                    results.Add(new ValidationResult("Description matches Category Name. Please make the description more detailed.", new string[] { "Description" }));
                }
            }
            if (!isValid)
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }

                return false;
            }

            return true;
        }

    }
}

