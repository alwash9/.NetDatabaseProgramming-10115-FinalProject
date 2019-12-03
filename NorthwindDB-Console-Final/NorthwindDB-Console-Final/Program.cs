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
            NLogger logger = new NLogger();
            logger.Log("INFO", "PROGRAM START!");
            Console.WriteLine();

            TopMenu menu = new TopMenu();
            menu.Start();


            Console.WriteLine("Goodbye");
            logger.Log("INFO", "PROGRAM END!");

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }
    }
}
