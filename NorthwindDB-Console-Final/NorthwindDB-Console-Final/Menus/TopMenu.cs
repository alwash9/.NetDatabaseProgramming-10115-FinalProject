using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDB_Console_Final.Menus
{
    class TopMenu
    {

        public void StartMenu()
        {
            Console.WriteLine("(1) Add new Product");

            if(Console.ReadKey().Key == ConsoleKey.D1 || Console.ReadKey().Key == ConsoleKey.NumPad1)
            {
                this.AddProductMenu();
            }
        }

        private void AddProductMenu()
        {
            string productName;
            string quantityPerUnit;
            decimal unitPrice;
            Int16 unitsInStock;
            Int16 unitsOnOrder;
            Int16 ReorderLevel;
            bool Discontinued;




            Console.WriteLine("Please enter the product's name.");
            productName = Console.ReadLine();

            Console.WriteLine("What is the quantity/unit for this product.");
            quantityPerUnit = Console.ReadLine();

            Console.WriteLine("What is the unit price for this product?\n\t*Note: This field allows null. Press nothing but ENTER to leave blank.");
            if()
            unitPrice = Console.ReadLine();


        }
    }
}
