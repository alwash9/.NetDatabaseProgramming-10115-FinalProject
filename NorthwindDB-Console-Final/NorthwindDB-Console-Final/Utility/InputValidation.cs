using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDB_Console_Final.Logging;

namespace NorthwindDB_Console_Final.Control
{
    class InputValidation
    {
        private NLogger logging = new NLogger();

        public int IntValidation(string input)
        {
            do
            {
                if (input == "")
                {
                    logging.Log("WARN", "Input was null and will be replaced with 0.");
                    return 0;
                }
                else if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    logging.Log("ERROR", "A valid input was not entered! Please enter a valid Integer number.");
                    Console.Write("Input?:\t"); input = Console.ReadLine();

                }

            } while (true);

        }


    }
}
