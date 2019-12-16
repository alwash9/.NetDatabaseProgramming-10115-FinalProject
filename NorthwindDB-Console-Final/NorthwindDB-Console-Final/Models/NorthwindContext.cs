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
                var toDelete = this.Products.FirstOrDefault(d => d.ProductID == product.ProductID);
                this.Products.Remove(toDelete);
                //var toDelete = this.Products.Attach(product);
                //this.Products.Remove(product);
                //this.Entry(product).State = EntityState.Deleted;
                this.SaveChanges();
                logger.Log("INFO", "Product has been deleted.");
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
                var toDelete = this.Categories.FirstOrDefault(d => d.CategoryId == category.CategoryId);
                this.Categories.Remove(toDelete);
                //db.Entry(product).State = EntityState.Deleted;
                this.SaveChanges();
                logger.Log("INFO", "Category has been deleted.");
            }
            catch (Exception ex)
            {
                logger.Log("ERROR", "An error occurred when attempting to delete a category.\n" + ex);
                throw;
            }

        }

        public void RemoveCategoryProducts(Category category)
        {
            NLogger logger = new NLogger();

            try
            {
                var toDelete = this.Categories.FirstOrDefault(d => d.CategoryId == category.CategoryId);
                this.Products.RemoveRange(toDelete.Products);
                //var toDelete = this.Products.Attach(product);
                //this.Products.Remove(product);
                //this.Entry(product).State = EntityState.Deleted;
                this.SaveChanges();
                logger.Log("INFO", "Products have been deleted.");
            }
            catch (Exception ex)
            {
                logger.Log("ERROR", "An error occurred when attempting to delete a product.\n" + ex);
                throw;
            }

        }


        public List<Product> SearchProducts(string searchName)
        {
            NLogger logging = new NLogger();
            var searchResult = this.Products.Where(p => p.ProductName.Contains(searchName)).ToList();

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

        public List<Product> SearchProducts(int searchNum)
        {
            NLogger logging = new NLogger();
            var searchResult = this.Products.Where(p => p.ProductID == searchNum).ToList();

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

        /// <summary>
        /// Search categories by name or description. 
        /// </summary>
        /// <param name="searchName"></param>
        /// <param name="type">true = by CategoryName false = Description</param>
        /// <returns></returns>
        public List<Category> SearchCategory(string searchName, bool type)
        {
            NLogger logging = new NLogger();
            List<Category> searchResult;


            if (type == true)
            {
                searchResult = this.Categories.Where(c => c.CategoryName.Contains(searchName)).ToList();

                if (searchResult.Count() == 0)
                {

                    logging.Log("WARN", "There were no categories found.");

                }
                else
                {
                    logging.Log("INFO", searchResult.Count() + " Categories Found.");

                }
            }

            else
            {
                searchResult = this.Categories.Where(c => c.Description.Contains(searchName)).ToList();

                if (searchResult.Count() == 0)
                {

                    logging.Log("WARN", "There were no categories found.");

                }
                else
                {
                    logging.Log("INFO", searchResult.Count() + " Categories Found.");
                }

            }



            return searchResult;
        }

        public List<Category> SearchCategory(int searchNum)
        {
            NLogger logging = new NLogger();
            var searchResult = this.Categories.Where(c => c.CategoryId == searchNum).ToList();

            if (searchResult.Count() == 0)
            {

                logging.Log("WARN", "There were no categories found.");

            }
            else
            {
                logging.Log("INFO", searchResult.Count() + " Categories Found.");

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

