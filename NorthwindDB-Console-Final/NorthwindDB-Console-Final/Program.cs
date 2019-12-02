using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Logging;
using NorthwindDB_Console_Final.Menus;

namespace NorthwindDB_Console_Final
{
    public class Program
    {

        public static void Main(string[] args)
        {

            Console.WriteLine("Hello");
            TopMenu menu = new TopMenu();
            //ProductMenu menu2 = new ProductMenu();
            menu.Start();
            //menu2.StartMenu();

            Console.WriteLine("Goodbye");

            Console.ReadLine();
        }
    }
}
