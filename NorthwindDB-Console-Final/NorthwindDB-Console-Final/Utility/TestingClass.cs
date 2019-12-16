using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Models;


namespace NorthwindDB_Console_Final.Utility
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

            PropertyInfo[] mypropinfo;
            Type t = x.GetType();
            mypropinfo = t.GetProperties();

            Console.WriteLine(mypropinfo);
            for (int i = 0; i < mypropinfo.Length; i++)
            {
                Console.WriteLine(mypropinfo[i].Name);
                Console.WriteLine(mypropinfo[i].GetValue(x));

            }

            //System.Reflection.PropertyInfo[] myPropertyInfo;
            //// Get the properties of 'Type' class object.
            //myPropertyInfo = Type.GetType("NorthwindDB_Console_Final.Models.Category").GetProperties();
            //Console.WriteLine("Properties of System.Type are:");
            //for (int i = 0; i < myPropertyInfo.Length; i++)
            //{
            //    Console.WriteLine(myPropertyInfo[i].ToString());
            //}

        }
    }
}
