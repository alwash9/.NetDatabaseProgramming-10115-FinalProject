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
        public NorthwindContext() : base("name = NorthwindContext") { }

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

        public void AddCategory(Category category)
        {
            NLogger logger = new NLogger();
            try
            {
                this.Categories.Add(category);
                this.SaveChanges();
                logger.Log("INFO", "Category has been added.");
            }
            catch (Exception ex)
            {
                logger.Log("ERROR", "An error occurred when attempting to add a new category.\n" + ex);
                throw;
            }

        }

        public void RemoveProduct(Product product)
        {
            NLogger logger = new NLogger();

            try
            {
                NorthwindContext db = new NorthwindContext();
                var toDelete = db.Products.Attach(product);
                db.Products.Remove(toDelete);
                //db.Entry(product).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                logger.Log("ERROR", "An error occurred when attempting to delete a product.\n" + ex);
                throw;
            }

        }

        public void RemoveCategory(Category category)
        {
            NLogger logger = new NLogger();

            try
            {
                NorthwindContext db = new NorthwindContext();
                var toDelete = db.Categories.Attach(category);
                db.Categories.Remove(toDelete);
                //db.Entry(product).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                logger.Log("ERROR", "An error occurred when attempting to delete a category.\n" + ex);
                throw;
            }

        }

        //fix
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
                //Console.WriteLine($"{"Product ID",-10}Product Name\n");
                //foreach (var item in searchResult)
                //{
                //    Console.WriteLine($"{item.ProductID,-10}{item.ProductName}\n");
                //}

            }


            return searchResult;
        }

        public IQueryable<Product> SearchProducts(int searchNum)
        {
            NLogger logging = new NLogger();
            var searchResult = this.Products.Where(p => p.ProductID == searchNum);

            if (searchResult.Count() == 0)
            {

                logging.Log("WARN", "There were no products found.");

            }
            else
            {
                //Console.WriteLine($"{"Product ID",-10}Product Name\n");
                //foreach (var item in searchResult)
                //{
                //    Console.WriteLine($"{item.ProductID,-10}{item.ProductName}\n");
                //}

            }


            return searchResult;
        }

        /// <summary>
        /// Search categories by name or description. 
        /// </summary>
        /// <param name="searchName"></param>
        /// <param name="type">true = by CategoryName false = Description</param>
        /// <returns></returns>
        public IQueryable<Category> SearchCategory(string searchName, bool type)
        {
            NLogger logging = new NLogger();
            IQueryable<Category> searchResult;


            if (type == true)
            {
                searchResult = this.Categories.Where(c => c.Description.Contains(searchName));

                if (searchResult.Count() == 0)
                {

                    logging.Log("WARN", "There were no categories found.");

                }
                else
                {
                    //Console.WriteLine($"{"Product ID",-10}Product Name\n");
                    //foreach (var item in searchResult)
                    //{
                    //    Console.WriteLine($"{item.ProductID,-10}{item.ProductName}\n");
                    //}

                }
            }

            else
            {
                searchResult = this.Categories.Where(c => c.CategoryName.Contains(searchName));

                if (searchResult.Count() == 0)
                {

                    logging.Log("WARN", "There were no categories found.");

                }
                else
                {
                    //Console.WriteLine($"{"Product ID",-10}Product Name\n");
                    //foreach (var item in searchResult)
                    //{
                    //    Console.WriteLine($"{item.ProductID,-10}{item.ProductName}\n");
                    //}

                }

            }



            return searchResult;
        }

        public IQueryable<Category> SearchCategory(int searchNum)
        {
            NLogger logging = new NLogger();
            var searchResult = this.Categories.Where(c => c.CategoryId == searchNum);

            if (searchResult.Count() == 0)
            {

                logging.Log("WARN", "There were no categories found.");

            }
            else
            {
                //Console.WriteLine($"{"Product ID",-10}Product Name\n");
                //foreach (var item in searchResult)
                //{
                //    Console.WriteLine($"{item.ProductID,-10}{item.ProductName}\n");
                //}

            }


            return searchResult;
        }



        public void AddCategory2(Category category)
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

