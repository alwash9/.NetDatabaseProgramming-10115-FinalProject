using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;


namespace NorthwindDB_Console_Final.Control
{
    class TestingClass
    {
        public void Start()
        {
            NorthwindContext db = new NorthwindContext();
            var x = db.Products.FirstOrDefault();


            Console.WriteLine(x.ProductName);
            Console.WriteLine(x.CategoryId);
            Console.WriteLine(db.Categories.FirstOrDefault(c => c.CategoryId == x.CategoryId).CategoryName);
            Console.WriteLine(x.Category);
        }
    }
}
