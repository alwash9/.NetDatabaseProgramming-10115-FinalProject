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
        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {

            Console.WriteLine("Hello");
            //TopMenu menu = new TopMenu();
            //ProductMenu menu2 = new ProductMenu();
            //menu.StartMenu();
            //menu2.StartMenu();

            NLogger logger = new NLogger();

            //logger.Info("This is a test");
            logger.Log("Test", "ERROR");

            Console.WriteLine("Goodbye");

            Console.ReadLine();
        }
    }
}
